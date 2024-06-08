<script setup>
import CV_CONSTANT from '~/assets/js/constants/constants';
import { apiFactory } from '~/plugins/api';
import { splitArrayByStatusCV, getConstantFromColumnId, extractGmailOnly, isNonEmptyString, findIndexByAttribute, generateUuid } from '~/assets/js/helper.js';
import { useCvStore } from '~/stores/cvStore';
import { nextTick } from 'vue';
import { isArrayEmpty } from '~/assets/js/validate';
import { useToast } from 'vue-toast-notification';
const toast = useToast();
const cvStore = useCvStore();
//lưu trữ tất cả các CV
const { cvS } = storeToRefs(cvStore);
const columns = ref([]);
const key = ref(generateUuid());
const forceUpdate = () => {
    key.value = key.value + '1';
};

const updateCvStatusEvent = async (event, statusCV, column) => {
    console.log(columns.value)
    console.log(event)
    if (event.added) {
        //ko dùng đến idCvNeedUpdate
        let params = {
            StatusCv: statusCV,
            idCvNeedUpdate: event.added.element.id,
            columnId: column.id
        };

        //call api update status cv, not send mail
        await apiFactory.sendMail.updateStatusCv(params, event.added.element);

        let ColumnLayout = await apiFactory.columnLayout.getAllPopulate({});
        await nextTick();

        //update item in store as the status update :))
        if (findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', event.added.element.id) != -1) {
            let index = findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', event.added.element.id)
            if (index != -1) {
                cvStore.selectedCvsToSendMail[index].columnId = column.id
                cvStore.selectedCvsToSendMail[index].status = statusCV
            }
        }

        if (findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', event.added.element.id) != -1) {
            let index = findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', event.added.element.id)
            cvStore.emailsEditing.splice(index, 1)
        }
        await reloadColumn()
    }
};

const groupColumn = ref({
    groupGlobal: {
        name: 'groupWaiting',
        pull: true,
        put: true,
    },
    groupWaiting: {
        name: 'groupWaiting',
        pull: ['groupReviewCvFail', 'groupReviewCvPass'],
        put: false,
    },
    groupReviewCvFail: { name: 'groupReviewCvFail', pull: false, put: ['groupWaiting'] },
    groupReviewCvPass: {
        name: 'groupReviewCvPass',
        pull: ['interviewResFail', 'interviewResPass'],
        put: ['groupWaiting'],
    },
    interviewResFail: {
        name: 'interviewResFail',
        pull: false,
        put: ['groupReviewCvPass'],
    },
    interviewResPass: {
        name: 'interviewResPass',
        pull: ['interviewResBackup'],
        put: ['interviewResBackup', 'groupReviewCvPass'],
    },
    interviewResBackup: {
        name: 'interviewResBackup',
        pull: ['interviewResFail', 'interviewResPass'],
        put: ['interviewResPass'],
    },
});


const getAllColumn = async () => {
    let ColumnLayout = await apiFactory.columnLayout.getAllPopulate({});
    columns.value = ColumnLayout.data.responseData
}

const reloadColumn = async () => {
    await getAllColumn();
}

onBeforeMount(async () => {
    await getAllColumn();
});

const createColumn = async () => {
    const formData = new FormData();
    formData.append("columnName", "New Column");

    var res = await apiFactory.columnLayout.createNewColumnLayout({
        formData
    })
    await nextTick();

    if (res.data.statusCode === 200) {
        let ColumnLayout = await apiFactory.columnLayout.getAllPopulate({});
        columns.value = ColumnLayout.data.responseData;
    }
}


watch(() => columns.value, () => {
    if (columns.value.MailTo) {
    }
}, { deep: true })

const handleSendmails = async () => {
    if (isArrayEmpty(cvStore.selectedCvsToSendMail)) {
        toast.error('Lỗi: ' + "Vui lòng chọn CV để gửi mail");
        return;
    }

    var resArray = [];
    for (let i = 0; i < cvStore.selectedCvsToSendMail.length; i++) {
        if (findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', cvStore.selectedCvsToSendMail[i].id) != -1) {
            let index = findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', cvStore.selectedCvsToSendMail[i].id)
            cvStore.selectedCvsToSendMail[i].mailContent = cvStore.emailsEditing[index]
            cvStore.emailsEditing.splice(index, 1)
        }

        if (isNonEmptyString(cvStore.selectedCvsToSendMail[i].mailContent.EmailsCC)) {
            cvStore.selectedCvsToSendMail[i].mailContent.EmailsCC = [...cvStore.selectedCvsToSendMail[i].mailContent.EmailsCC.split(/\s,\s/).filter(Boolean)]
        }
        if (isNonEmptyString(cvStore.selectedCvsToSendMail[i].mailContent.EmailsBCC)) {
            cvStore.selectedCvsToSendMail[i].mailContent.EmailsBCC = [...cvStore.selectedCvsToSendMail[i].mailContent.EmailsBCC.split(/\s,\s/).filter(Boolean)]
        }
        resArray.push(cvStore.selectedCvsToSendMail[i])
    }
    var response = await apiFactory.cv.sendMailssBaseOnStatus(resArray)

    //xoa cac email da gui ra khoi store
    cvStore.selectedCvsToSendMail = []
    let ColumnLayout = await apiFactory.columnLayout.getAllPopulate({});

    columns.value = ColumnLayout.data.responseData
}

</script>

<template>
    <!-- <CVcombobox></CVcombobox> -->
    <button type="button" class="sticky-bottom btn btn-success" @click="handleSendmails">Gửi mails</button>
    <button type="button" class="sticky-bottom btn btn-primary ms-3" @click="createColumn">
        Thêm cột</button>

    <div class="container-custom overflow-auto  p-3 ps-0 row">
        <div class="d-flex align-items-start overflow-x-auto gx-2 row flex-nowrap"
            style="min-width: 70vw; min-height: 77vh;" :key="key">

            <the-kanban-column v-for="element in columns" @column-deleted="reloadColumn"
                :log="(event) => updateCvStatusEvent(event, getConstantFromColumnId(element.id), element)"
                :itemsDraggable="element" :group="groupColumn.groupGlobal">
                <template #name-column>
                </template>
            </the-kanban-column>

        </div>
    </div>
</template>
<style scoped>
.wrapper {
    height: 100vh;

    display: grid;
    grid-gap: 5px;

    grid-template-rows: 30px auto;
    grid-template-columns: auto auto auto;
    grid-template-areas:
        'todo-caption wip-caption done-caption'
        'todo wip done';
}

.table-todo {
    grid-area: todo;
}

.table-done {
    grid-area: done;
}

.table-wip-caption {}

.focus-bg-white:focus {
    background-color: white !important;
}

.dragArea {
    /**/
    min-height: 4px;
    /**/
    /**/
    /* min-height: var(--height-dragArea); */
    height: 100%;
    width: 100%;
    /**/

    /**/
}

/*********************************/
</style>
