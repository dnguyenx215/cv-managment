<script setup>
import { ref, defineProps, defineEmits } from 'vue'
import { apiFactory } from '~/plugins/api';
import { isNonEmptyString, isNotNumber, generateUuid, useDebounce, findIndexByAttribute, areObjectsEqual } from '~/assets/js/helper.js';
import { useToast } from 'vue-toast-notification';
import { validateCvInfoBeforeSendingMail, formatDateYYYYmmdd } from '~/assets/js/validate';
import CV_CONSTANT from '~/assets/js/constants/constants';
import { useCvStore } from '~/stores/cvStore';
import API from '~/service/Base/api';
const api = new API();
const cvStore = useCvStore();
const toast = useToast();
const props = defineProps({
    data_bs_target_modal: {
        type: String,
        default: '',
    },
    id_model: {
        type: String,
        default: '',
    },

    cv_data: {
        type: Object,
        require: true,
        default: {}
    }
});

const keyMailContent = ref(generateUuid());
const updateMailContentValue = () => {
    keyMailContent.value = keyMailContent.value + "1"
}
let debounceUpdateMailContentValue = useDebounce(() => {
    updateMailContentValue()
}, 500);
const mailData = defineModel('mailData')
const mailDataPreview = ref(props.cv_data.mailContent)

//Biến check do watch trả về newMailContent = oldMailContent, ko thay đổi được
let oldSalary = 0

onBeforeMount(async () => {
    //lấy template lần đầu tiên
    getEmailTemplate()
    await nextTick()


})


//watch trả về newMailContent = oldMailContent với cách viết ({...mailDataPreview.value})
watch(() => ({ ...mailDataPreview.value }), async (newMailContent, oldMailContent) => {
    if (isNonEmptyString(newMailContent.EmailsCC)) {
        newMailContent.EmailsCC = [...newMailContent.EmailsCC.split(/\s*,\s*/).filter(Boolean)]
    }
    if (isNonEmptyString(newMailContent.EmailsBCC)) {
        newMailContent.EmailsBCC = [...newMailContent.EmailsBCC.split(/\s*,\s*/).filter(Boolean)]
    }

    if (newMailContent.Content) {
        // Extract the text before the first <span> tag
        let beforeSpan = newMailContent.Content.split('<span>')[0];

        // Extract the text after the last </span> tag
        let afterSpan = newMailContent.Content.split('</span>').pop();

        // Replace the text between <span> and </span> tags
        let newText = newMailContent.Salary;
        if (!isNonEmptyString(newText)) {
            newText = '0'
        }
        let newMailContentTemp = `${beforeSpan}<span>${newText}</span>${afterSpan}`;
        // Construct the new mail content
        if (newMailContent.Salary != '0') {
            mailDataPreview.value.Content = newMailContentTemp
            if (oldSalary != newMailContent.Salary) {
                debounceUpdateMailContentValue()
                await nextTick();
                oldSalary = newMailContent.Salary
            }
        }

        let mailtemplateData = await apiFactory.cv.getEmailTemplate(props.cv_data.status, props.cv_data);
        let index = findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', props.cv_data.id)
        if (!areObjectsEqual(newMailContent, mailtemplateData.data)) {
            if (findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', props.cv_data.id) != -1) {
                cvStore.emailsEditing[index] = newMailContent
            }
            else {
                cvStore.emailsEditing.push(newMailContent)
            }
        }
        else {

            if (findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', props.cv_data.id) != -1) {
                cvStore.emailsEditing.splice(index, 1)
            }

        }
    }

}, { deep: true });


const handleSubmit = async (event) => {
    // mailData.value.EmailsCC = [...mailData.value.EmailsCC.split(",")];
    // mailData.value.EmailsBCC = [...mailData.value.EmailsBCC.split(",")];
    if (validateCvInfoBeforeSendingMail(props.cv_data).length > 0) {
        let message = validateCvInfoBeforeSendingMail(props.cv_data).join('\n')
        toast.error('Lỗi: ' + message);
        return;
    }
    if (isNonEmptyString(mailDataPreview.value.EmailsCC)) {
        mailDataPreview.value.EmailsCC = [...mailDataPreview.value.EmailsCC.split(/\s*,\s*/).filter(Boolean)]
    }
    if (isNonEmptyString(mailDataPreview.value.EmailsBCC)) {
        mailDataPreview.value.EmailsBCC = [...mailDataPreview.value.EmailsBCC.split(/\s*,\s*/).filter(Boolean)]
    }

    props.cv_data.mailContent = mailDataPreview.value

    var response = await apiFactory.cv.sendMailBaseOnStatus(props.cv_data.id, (props.cv_data))
    if (Math.floor(response.data.statusCode / 100) == 2) {
        $('.btn-close').click();
    }
    //Xoa item khoi store cac CV muon gui mail hang loat


    if (findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', mailDataPreview.value.IdCvNeedToSendMail) != -1) {
        let index = findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', mailDataPreview.value.IdCvNeedToSendMail)
        cvStore.selectedCvsToSendMail.splice(index, 1)
    }
    if (findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', mailDataPreview.value.IdCvNeedToSendMail) != -1) {

        let index = findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', mailDataPreview.value.IdCvNeedToSendMail)
        cvStore.emailsEditing.splice(index, 1)
    }
    await getEmailTemplate()
}
async function getEmailTemplate() {

    props.cv_data.dateReceiveCV = formatDateYYYYmmdd(props.cv_data.dateReceiveCV);
    if (props.cv_data.dateOfInterview) {
        props.cv_data.dateOfInterview = formatDateYYYYmmdd(props.cv_data.dateOfInterview);
    }
    if (props.cv_data.dateAcceptJob) {
        props.cv_data.dateAcceptJob = formatDateYYYYmmdd(props.cv_data.dateAcceptJob);
    }

    if (findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', props.cv_data.id) != -1) {
        //if cv is selected for sending mail, return history edited mail content
        let index = findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', props.cv_data.id)


        mailDataPreview.value = cvStore.emailsEditing[index]
        await nextTick()
    }
    else {
        //get mail template for the first time
        let mailtemplateData = await apiFactory.cv.getEmailTemplate(props.cv_data.status, props.cv_data);
        mailDataPreview.value = mailtemplateData.data
        await nextTick()
    }

    if (mailDataPreview.value) {
        mailDataPreview.value.EmailsCC = CV_CONSTANT.GMAIL.CC_DEFAULT;
    }
    updateMailContentValue()
}




function isDisplaySalary(statusCv) {
    switch (statusCv) {
        case CV_CONSTANT.REVIEW_CV_WAITING:
            return false;
        case CV_CONSTANT.REVIEW_CV_FAIL:
            return false;
        case CV_CONSTANT.REVIEW_CV_PASS:
            return false;
        case CV_CONSTANT.INTERVIEW_RES_FAIL:
            return false;
        case CV_CONSTANT.INTERVIEW_RES_PASS:
            return true;
        case CV_CONSTANT.INTERVIEW_RES_BACKUP:
            return false;
        default:
            // Handle unknown status
            break;
    }
};

</script>
<template>

    <CVModal :id_model="id_model" :data_bs_target_modal="data_bs_target_modal"
        @hide-modal="debounceUpdateMailContentValue">
        <template #icon>
            <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
                Launch demo modal
            </button> -->
            <slot name="icon"></slot>
        </template>

        <template #title>
            Chi tiết Email
        </template>

        <template #body>
            <div>
                <!-- {{ cv_data.mailContent.mailTo }} -->
                <!-- {{ mailDataPreview.MailTo }} -->
                <Form role="form" class="form-horizontal" @submit="handleSubmit">
                    <div class="form-group mb-4">
                        <label class="col-lg-2 control-label">To</label>
                        <div class="col-lg-12">
                            <!-- <input type="text" placeholder="" id="inputEmail1" class="form-control"
                            v-model="mailData.MailTo"> -->
                            <Field :rules="{ required: true }" class="form-control" type="text" name="MailTo"
                                v-model="mailDataPreview.MailTo" id="MailTo" placeholder="Gửi mail tới" />
                            <ErrorMessage name="MailTo" class="text-danger" />
                        </div>
                    </div>
                    <div class="form-group mb-4">
                        <label class="col-lg-2 control-label">Cc</label>
                        <div class="col-lg-12">
                            <input type="text" placeholder="" id="EmailsCC" class="form-control"
                                v-model="mailDataPreview.EmailsCC">
                            <!-- <v-select :options="options" /> -->
                        </div>
                    </div>
                    <div class="form-group mb-4">
                        <label class="col-lg-2 control-label">Bcc</label>
                        <div class="col-lg-12">
                            <input type="text" placeholder="" id="EmailsBCC" class="form-control"
                                v-model="mailDataPreview.EmailsBCC">
                        </div>
                    </div>
                    <div class="form-group mb-4" v-show="isDisplaySalary(cv_data.status)">
                        <label class="col-lg-2 control-label">Lương</label>
                        <div class="col-lg-12">
                            <div class="input-group">
                                <div class="input-group mb-3">
                                    <span class="input-group-text" id="basic-addon1">VND</span>

                                    <CVInputSalary v-if="mailDataPreview.Salary" v-model:mailData="mailDataPreview">
                                    </CVInputSalary>
                                    <!-- <input id="amount" name="amount" type="text" maxlength="15" v-model="computedValue"
                                    class="form-control" placeholder="Lương" aria-label="Lương" @input="handleInput" /> -->
                                    <span class="input-group-text"><i class="fa fa-times"></i></span>

                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-4">
                        <label class="col-lg-2 control-label">Subject</label>
                        <div class="col-lg-12">
                            <Field :rules="{ required: true }" class="form-control" type="text" name="Subject"
                                v-model="mailDataPreview.Subject" id="Subject" placeholder="Tên ứng viên" />
                            <ErrorMessage name="Subject" class="text-danger" />
                        </div>
                    </div>
                    <div class="form-group mb-4">
                        <label class="col-lg-2 control-label">Message</label>
                        <div class="col-lg-12">
                            <!-- <textarea rows="10" cols="30" class="form-control" id="" name=""
                            v-model="mailDataPreview.Content"></textarea> -->
                            <!-- <div class="border border-gray-400 h-50" v-html="mailDataPreview.Content"></div> -->
                            <TipTap :key="keyMailContent" v-model="mailDataPreview.Content"></TipTap>
                            <!-- <textarea v-model="mailDataPreview.Content"></textarea> -->
                        </div>
                    </div>

                    <div class="form-group ">
                        <div class="col-lg-offset-2 col-lg-12">
                            <button class="btn btn-send" type="submit">Send</button>
                        </div>
                    </div>
                </Form>
            </div>
        </template>
    </CVModal>
</template>
<style scoped>
.btn-send,
.btn-send:hover {
    background: none repeat scroll 0 0 #00a8b3;
    color: #fff;
}

.btn-send:hover {
    background: none repeat scroll 0 0 #009da7;
}

.modal-header h4.modal-title {
    font-family: "Open Sans", sans-serif;
    font-weight: 300;
}

.modal-body label {
    font-family: "Open Sans", sans-serif;
    font-weight: 400;
}

.heading-inbox h4 {
    border-bottom: 1px solid #ddd;
    color: #444;
    font-size: 18px;
    margin-top: 20px;
    padding-bottom: 10px;
}

.sender-dropdown {
    background: none repeat scroll 0 0 #eaeaea;
    color: #777;
    font-size: 10px;
    padding: 0 3px;
}

.fileinput-button {
    float: left;
    margin-right: 4px;
    overflow: hidden;
    position: relative;
}

.fileinput-button input {
    cursor: pointer;
    direction: ltr;
    font-size: 23px;
    margin: 0;
    opacity: 0;
    position: absolute;
    right: 0;
    top: 0;
    transform: translate(-300px, 0px) scale(4);
}

.fileupload-buttonbar .btn,
.fileupload-buttonbar .toggle {
    margin-bottom: 5px;
}

.files .progress {
    width: 200px;
}

.fileupload-processing .fileupload-loading {
    display: block;
}

* html .fileinput-button {
    line-height: 24px;
    margin: 1px -3px 0 0;
}

*+html .fileinput-button {
    margin: 1px 0 0;
    padding: 2px 15px;
}


ul {
    list-style-type: none;
    padding: 0px;
    margin: 0px;
}

.container {
    max-width: 600px;
    margin: 0 auto;
    background-color: white;
    padding: 20px;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

h1 {
    text-align: center;
    color: #f44336;
}

p {
    line-height: 1.5;
}

.btn {
    background-color: #055ff0;
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    display: block;
    margin: 20px auto;
    text-decoration: none;
    text-align: center;
}

.btn:hover {
    background-color: #45a049;
}
</style>
