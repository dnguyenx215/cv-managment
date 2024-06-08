<script setup>
import {
    capitalize,
    getVietNameseText,
    formatDate,
    formatTime,
    formatDateWithTime,
    getBackgroundColor,
    areObjectsEqual
} from '~/assets/js/helper.js';
import API from '~/service/Base/api';
import CV_CONSTANT from '~/assets/js/constants/constants';
import { apiFactory } from '~/plugins/api';
import { storeToRefs } from 'pinia';
import { useCvStore } from '~/stores/cvStore';
import { findIndexByAttribute, updateById, generateUuid, isNonEmptyString } from '~/assets/js/helper.js';
import { validateCvInfoBeforeSendingMail } from '~/assets/js/validate';
import { useToast } from 'vue-toast-notification';

const toast = useToast();
const api = new API();
const cvStore = useCvStore();
//Lưu trữ danh sách các cv được select để gưi
const { selectedCvsToSendMail } = storeToRefs(cvStore);
const props = defineProps({
    isGroup: {
        type: Boolean,
        default: true,
    },
    name: {
        type: String,
        default: 'true',
    },
    modalItem: {
        type: Object,
        default: {},
    },
    isSendMail: {
        type: Boolean,
        default: false,
    }
});
const positionData = ref(null);
const mailTemplate = ref(null)
const dataModal = ref(generateUuid());
const dataModal1 = ref(generateUuid());
const flagId = ref(generateUuid())
const checkboxKey = ref(generateUuid())

const isUpdateStatusCv = ref(false)
const oldMailTemplate = ref({})
const updateMailPreview = () => {
    dataModal1.value = dataModal1.value + "1"
}
const updateKanbanItemLikeTrello = () => {
    dataModal.value = dataModal.value + "1"
}
const updateCheckboxKey = () => {
    checkboxKey.value = dataModal1.value + "1"
}

const updateFlagKanbanItem = () => {
    flagId.value = flagId.value + "1"
}




onBeforeMount(async () => {
    if (findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', props.modalItem.id) != -1) {
        props.modalItem.isSelectedToSendMail = true
        updateCheckboxKey()
    }
})

onBeforeMount(async () => {
    //Call API
    const positionAPI = await api.get('/Position');
    positionData.value = positionAPI.data.responseData;

    //remove event listener pointerdown
    const modelDetailCv = document.querySelector(`#update${props.name}`);
    const modelMail = document.querySelector(`#show${props.name}`);

    if (modelDetailCv) {
        modelDetailCv.addEventListener('pointerdown', (e) => {
            e.stopPropagation();
        });
    }
    if (modelMail) {
        modelMail.addEventListener('pointerdown', (e) => {
            e.stopPropagation();
        });
    }
    //updateMailPreview()
});


onUpdated(async () => {
    console.log(props.modalItem)
    const modelDetailCv = document.querySelector(`#update${props.name}`);
    const modelMail = document.querySelector(`#show${props.name}`);

    if (modelDetailCv) {
        modelDetailCv.addEventListener('pointerdown', (e) => {
            e.stopPropagation();
        });
    }
    if (modelMail) {
        modelMail.addEventListener('pointerdown', (e) => {
            e.stopPropagation();
        });
    }
});

const isDisplayTimeInterviewBaseOnStatusCv = (statusCv) => {
    switch (statusCv) {
        case CV_CONSTANT.REVIEW_CV_WAITING:
            return false;
        case CV_CONSTANT.REVIEW_CV_FAIL:
            return false;
        case CV_CONSTANT.REVIEW_CV_PASS:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_FAIL:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_PASS:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_BACKUP:
            return true;
        default:
            // Handle unknown status
            break;
    }
};

//Xử lý cho mail đã tồn tại trong store, khi chuyển trang vẫn sẽ lưu lại mail đã sửa
const isMailContentEdited = (idCv) => {
    var index = findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', idCv)
    if (index != -1) {
        return true
    }
    return false
};

const isDisplayRateOnItem = (statusCv) => {
    switch (statusCv) {
        case CV_CONSTANT.REVIEW_CV_WAITING:
            return false;
        case CV_CONSTANT.REVIEW_CV_FAIL:
            return false;
        case CV_CONSTANT.REVIEW_CV_PASS:
            return false;
        case CV_CONSTANT.INTERVIEW_RES_FAIL:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_PASS:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_BACKUP:
            return true;
        default:
            // Handle unknown status
            break;
    }
};

const isDisplayMailIconOnItem = (statusCv) => {
    switch (statusCv) {
        case CV_CONSTANT.REVIEW_CV_WAITING:
            return false;
        case CV_CONSTANT.REVIEW_CV_FAIL:
            return true;
        case CV_CONSTANT.REVIEW_CV_PASS:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_FAIL:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_PASS:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_BACKUP:
            return true;
        default:
            // Handle unknown status
            break;
    }
};


const isDisplayTimeOnBoardBaseOnStatusCv = (statusCv) => {
    if (statusCv == CV_CONSTANT.INTERVIEW_RES_PASS) {
        return true;
    }
    return false;
};



watch(
    () => props.modalItem,
    async (newVal, oldVal) => {
        // let mailtemplateData = await apiFactory.cv.getEmailTemplate(newVal.status, newVal);

        //same status = same mail data edit
        if (oldVal.status == newVal.status) {
            await nextTick();
            props.modalItem.isEmailContendUpdated = false
        }
        else {
            updateKanbanItemLikeTrello()
            await nextTick();
        }

        //update checkbox
        if (findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', props.modalItem.id) != -1) {
            props.modalItem.isSelectedToSendMail = true
            updateCheckboxKey()
        }
        $('.btn-close').click();
        updateKanbanItemLikeTrello()
    }
);

function onClickCheckbox(e) {
    if (validateCvInfoBeforeSendingMail(props.modalItem).length > 0) {
        let message = validateCvInfoBeforeSendingMail(props.modalItem).join('\n')
        toast.error('Lỗi: ' + message);
        e.target.checked = false
        return;
    }

    if (e.target.checked) {
        cvStore.selectedCvsToSendMail.push(props.modalItem)
    }
    else if (!e.target.checked) {
        let index = findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', props.modalItem.id)
        if (index != -1) {
            cvStore.selectedCvsToSendMail.splice(index, 1)
        }
    }
}
</script>
<template>
    <div class="container-ext">
        <div class="group-name card shadow-sm card-body p-1" style="padding: 1px" :key="dataModal">
            <the-kanban-item-like-trello :idCv="modalItem.id" :cvData="modalItem"
                :data_bs_target_modal="'#update' + name" :id_model="'update' + name"
                @getEmailTemplate="getEmailTemPlateWhenUpdateCvIf">
                <template #icon>
                    <h6 class="card-title text-center">
                        {{ modalItem.nameCandidate }} <br>
                        <span v-for="position in positionData" class=" text-primary">
                            <span v-if="position.id == modalItem.positionId">
                                {{ position.positionName }}
                            </span>
                        </span>
                    </h6>
                    <div class="row mt-2">
                        <div class="col-5  card-subtitle  text-muted p-0 pe-1 ps-4" data-bs-toggle="tooltip"
                            data-bs-placement="top" title="Thời gian nhận CV ">
                            Nhận CV :
                        </div>
                        <div class="col-7 font-size-12  card-subtitle p-0 ps-1 ">
                            {{
                                formatDate(modalItem.dateReceiveCV)
                            }}

                        </div>
                    </div>
                    <div class="row " v-if="
                        isDisplayTimeInterviewBaseOnStatusCv(modalItem.status)
                    ">
                        <div class="col-4 card-subtitle  text-muted p-0 ps-4" data-bs-toggle="tooltip"
                            data-bs-placement="top" title="Thời gian phỏng vấn">
                            Time :
                        </div>
                        <div class="col-8 font-size-12 card-subtitle p-0 ps-4  ">
                            {{
                                modalItem.timeOfInterview
                                    ? modalItem.timeOfInterview
                                    : 'N/A'
                            }}
                            {{
                                modalItem.timeOfInterview ? formatDate(modalItem.dateOfInterview) : 'N/A'
                            }}
                        </div>
                    </div>


                    <div class="row gx-2" v-if="
                        isDisplayTimeOnBoardBaseOnStatusCv(modalItem.status)
                    ">
                        <div class="col-4 card-subtitle  text-muted p-0 ps-3 " data-bs-toggle="tooltip"
                            data-bs-placement="top" title="Thời gian đi làm">
                            Onb :
                        </div>
                        <div class="col-8 font-size-12 card-subtitle p-0 ps-4  ">
                            {{
                                modalItem.dateAcceptJob ? formatDate(modalItem.dateAcceptJob) : 'N/A'

                            }}

                        </div>
                    </div>

                </template>
                <template #header>Update</template>
            </the-kanban-item-like-trello>

            <div class="card-footer mt-2 p-1">
                <div class="row ">
                    <div class="col-6 "><i class="fa-solid fa-link ms-3" style="font-size:12px;"></i>
                        <a :href="modalItem.sourceOfCV" target="_blank" class=" ms-1 text-decoration-none">Link CV</a>
                    </div>
                    <div class="col-6 d-flex justify-content-end" v-if="isDisplayRateOnItem(modalItem.status)">
                        <i class="fa-solid fa-ranking-star ms-4 me-1 " style="color:crimson"></i>
                        <span> {{ modalItem.rate }}</span>
                    </div>
                </div>

                <div class="mt-2 ms-3 d-flex justify-content-between">
                    <the-form-preview-and-send-mail :key="dataModal1" :data_bs_target_modal="'#show' + name"
                        :id_model="'show' + name" :cv_data="modalItem" v-if="isDisplayMailIconOnItem(modalItem.status)">
                        <template #icon>
                            <div :key="flagId">
                                <span v-if="!isMailContentEdited(modalItem.id)">
                                    <i class="fa-solid fa-envelope text-success"></i>
                                </span>
                                <span v-else>
                                    <i class="bi bi-envelope-exclamation-fill fs-5 text-danger"></i>
                                </span>
                            </div>

                        </template>
                        <template #header>Xem chi tiết mail</template>
                    </the-form-preview-and-send-mail>
                    <section>
                        <input class="form-check-input me-2 border-primary " type="checkbox"
                            v-model="modalItem.isSelectedToSendMail" id="flexCheckDefault" @click="onClickCheckbox"
                            :key="checkboxKey" />
                        <div :class="[getBackgroundColor(modalItem.isSendMail)]">
                            {{ getVietNameseText(modalItem.isSendMail) }}
                        </div>
                    </section>

                </div>
            </div>
        </div>
    </div>
</template>


<style scoped>
* {
    box-sizing: border-box;
}

html {
    font-family: sans-serif;
}


.container-ext {
    min-width: 150px;
    min-height: 80px;
    transition: 0.15s ease-out;

}

.container-ext::before {}

.container-ext:hover {
    cursor: pointer;
    border: 1px solid #09fa29e4;

}

p {
    margin-bottom: 0.5rem;
}

.font-size-12 {
    font-size: 0.95rem;
}
</style>
