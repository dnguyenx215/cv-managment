<template>
    <CVModal :data_bs_target_modal="data_bs_target_modal" :id_model="id_model">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Vị Trí </slot>
        </template>
        <template #body>
            <Form @submit="submitForm">
                <div class="">
                    <div class="form-create row">
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="name" class="col-sm-4 col-form-label control-label text-end">Tên</label>
                            <div class="col-sm-8">
                                <Field type="text" name="name" class="form-control" :rules="{ required: true }"
                                    v-model="request.nameCandidate" id="name" />
                                <ErrorMessage name="name" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="dateReceiveCV" class="col-sm-4 col-form-label control-label text-end">Nhận
                                CV</label>
                            <div class="col-sm-8">
                                <Field type="date" name="dateReceiveCV" class="form-control"
                                    v-model="request.dateReceiveCV" id="dateReceiveCV" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="phone-number" class="col-sm-4 col-form-label control-label text-end"><a
                                    class="phone-link" :href="`tel://${request.phoneNumber}`">Điện thoại</a></label>
                            <div class="col-sm-8">
                                <Field type="text" class="form-control" name="phone-number"
                                    :rules="{ required: true, phone: true }" v-model="request.phoneNumber"
                                    id="phone-number" />
                                <ErrorMessage name="phone-number" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="job-position" class="col-sm-4 col-form-label control-label text-end">Vị
                                trí</label>
                            <div class="col-sm-8">
                                <Field as="select" required class="form-control form-select" :rules="{ required: true }"
                                    v-model="request.positionId" aria-label="Default select example" name="job-position"
                                    id="job-position">
                                    <option :rules="{ required: true }" name="job-position"
                                        v-for="position in positionData" :value="position.id">
                                        {{ position.positionName }}
                                    </option>
                                </Field>
                                <ErrorMessage name="job-position" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="school" class="col-sm-4 col-form-label control-label text-end">Trường</label>
                            <div class="col-sm-8">
                                <Field type="text" name="school" :rules="{ required: true }" class="form-control"
                                    v-model="request.school" id="school" />
                                <ErrorMessage name="school" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="source" class="col-sm-4 col-form-label control-label text-end">Nguồn CV</label>
                            <div class="col-sm-8">
                                <Field as="select" id="source" :rules="{ required: true }" name="source"
                                    class="form-control form-select" aria-label="Default select example"
                                    v-model="request.sourceId">
                                    <option v-for="source in sourceData" :value="source.id">
                                        {{ source.nameSource }}
                                    </option>
                                </Field>
                                <ErrorMessage name="source" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="birthday" class="col-sm-4 col-form-label control-label text-end">Năm
                                sinh</label>
                            <div class="col-sm-8">
                                <Field type="text" name="birthday" :rules="{ required: true }" class="form-control"
                                    v-model="request.yearOfBirth" id="birthday" />
                                <ErrorMessage name="birthday" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="link-source" class="col-sm-4 col-form-label text-end">
                                <span><a :href="request.sourceOfCV" class="nav-link control-label phone-link"
                                        target="”_blank”">Link CV</a></span>
                            </label>

                            <div class="col-sm-8">
                                <Field type="text" class="form-control" name="link-source" :rules="{ required: true }"
                                    id="link-source" v-model="request.sourceOfCV" />
                                <ErrorMessage name="link-source" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group required">
                            <label for="school" class="col-sm-4 col-form-label control-label text-end">Email</label>
                            <div class="col-sm-8">
                                <Field type="email" class="form-control" name="email"
                                    :rules="{ required: true, email: true }" v-model="request.email" id="email" />
                                <ErrorMessage name="email" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="note" class="col-sm-4 col-form-label control-label text-end">Note</label>
                            <div class="col-sm-8">
                                <textarea class="form-control note-cv" name="note" id="note" @input="adjustHeight"
                                    v-model="request.cvNote"></textarea>
                            </div>
                        </div>
                        <div v-if="props.idCv == null">
                            <div class="row mb-3 col-md-6 form-group">
                                <label for="note" class="col-sm-4 col-form-label control-label text-end">File CV</label>
                                <div class="col-sm-8">
                                    <input type="file" @change="handleFileChange" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-if="props.idCv != null" class="form-update row">
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="status-review" class="col-sm-4 col-form-label control-label text-end">Review
                                CV</label>
                            <div class="col-sm-8">
                                <textarea class="form-control note-cv" id="reviewCV"
                                    v-model="request.reviewCV"></textarea>
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="interview-time" class="col-sm-4 col-form-label control-label text-end">Đánh
                                giá</label>
                            <div class="col-sm-8">
                                <textarea class="form-control note-cv" v-model="request.cvEvaluate"></textarea>
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="interview-time" class="col-sm-4 col-form-label control-label text-end">Thời gian
                                PV</label>
                            <div class="col-sm-8">
                                <input type="time" class="form-control" v-model="request.timeOfInterview" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="rate" class="col-sm-4 col-form-label control-label text-end">Điểm</label>
                            <div class="col-sm-8">
                                <Field class="form-control" :rules="{ point: true }" id="rate" name="rate"
                                    v-model="request.rate" />
                                <ErrorMessage name="rate" class="text-danger" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="interview-time" class="col-sm-4 col-form-label control-label text-end">Ngày
                                PV</label>
                            <div class="col-sm-8">
                                <input type="date" class="form-control" v-model="request.dateOfInterview" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="status" class="col-sm-4 col-form-label control-label text-end">Tình
                                trạng</label>
                            <div class="col-sm-8">
                                <select id="status-review" class="form-select" aria-label="Default select example"
                                    v-model="request.status">
                                    <option :value="constants.REVIEW_CV_WAITING">
                                        {{ constants.REVIEW_CV_WAITING }}
                                    </option>
                                    <option :value="constants.REVIEW_CV_FAIL">
                                        {{ constants.REVIEW_CV_FAIL }}
                                    </option>
                                    <option :value="constants.REVIEW_CV_PASS">
                                        {{ constants.REVIEW_CV_PASS }}
                                    </option>
                                    <option :value="constants.INTERVIEW_RES_FAIL">
                                        {{ constants.INTERVIEW_RES_FAIL }}
                                    </option>
                                    <option :value="constants.INTERVIEW_RES_PASS">
                                        {{ constants.INTERVIEW_RES_PASS }}
                                    </option>
                                    <option :value="constants.INTERVIEW_RES_BACKUP">
                                        {{ constants.INTERVIEW_RES_BACKUP }}
                                    </option>
                                    <option :value="constants.GET_JOB">
                                        {{ constants.GET_JOB }}
                                    </option>
                                    <option :value="constants.REFUSED_TO_BE_INTERVIEWED">
                                        {{ constants.REFUSED_TO_BE_INTERVIEWED }}
                                    </option>
                                    <option :value="constants.REFUSE_TO_ACCEPT_JOB">
                                        {{ constants.REFUSE_TO_ACCEPT_JOB }}
                                    </option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-3 col-md-6 form-group">
                            <label for="date-start-work" class="col-sm-4 col-form-label control-label text-end">Ngày đi
                                làm</label>
                            <div class="col-sm-8">
                                <input type="date" class="form-control" id="date-start-work"
                                    v-model="request.dateAcceptJob" />
                            </div>
                        </div>
                        <div class="row mb-3 col-md-6 form-group">
                            <label for="date-start-work" class="col-sm-4 col-form-label control-label text-end">Gửi
                                Mail</label>
                            <div class="col-sm-8">
                                <input type="text" disabled class="form-control" id="date-start-work"
                                    v-model="request.isSendMail" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <!-- slot footer here -->
                    <slot name="footer"></slot>
                    <button type="submit" class="btn btn-primary d-flex align-items-center">
                        <svg xmlns="http://www.w3.org/2000/svg" width="0.88em" height="1em" viewBox="0 0 448 512"
                            class="me-1">
                            <path fill="#f7f7f7"
                                d="m433.941 129.941l-83.882-83.882A48 48 0 0 0 316.118 32H48C21.49 32 0 53.49 0 80v352c0 26.51 21.49 48 48 48h352c26.51 0 48-21.49 48-48V163.882a48 48 0 0 0-14.059-33.941M224 416c-35.346 0-64-28.654-64-64c0-35.346 28.654-64 64-64s64 28.654 64 64c0 35.346-28.654 64-64 64m96-304.52V212c0 6.627-5.373 12-12 12H76c-6.627 0-12-5.373-12-12V108c0-6.627 5.373-12 12-12h228.52c3.183 0 6.235 1.264 8.485 3.515l3.48 3.48A11.996 11.996 0 0 1 320 111.48">
                            </path>
                        </svg>
                        Lưu
                    </button>
                    <button type="button" class="btn btn-outline-primary d-flex align-items-center"
                        data-bs-dismiss="modal">
                        <svg xmlns="http://www.w3.org/2000/svg" width="0.88em" height="1em" viewBox="0 0 448 512"
                            class="me-1 icon">
                            <path fill="#006eff"
                                d="m257.5 445.1l-22.2 22.2c-9.4 9.4-24.6 9.4-33.9 0L7 273c-9.4-9.4-9.4-24.6 0-33.9L201.4 44.7c9.4-9.4 24.6-9.4 33.9 0l22.2 22.2c9.5 9.5 9.3 25-.4 34.3L136.6 216H424c13.3 0 24 10.7 24 24v32c0 13.3-10.7 24-24 24H136.6l120.5 114.8c9.8 9.3 10 24.8.4 34.3">
                            </path>
                        </svg>
                        Huỷ bỏ
                    </button>
                </div>
            </Form>
        </template>
    </CVModal>
</template>
<script setup>
import { ref } from 'vue';
import API from '../../service/Base/api.ts';
import { format } from 'date-fns';
import { Form, Field, ErrorMessage } from 'vee-validate';
import { jwtDecode } from 'jwt-decode';
import constants from '../../assets/js/constants/constants.js';
import {
    checkIfAllowedUpdateStatus,
    getAllowedDisplayTimeAndDateInterview,
} from '~/assets/js/helper.js';
import { useRouter, useRoute } from 'nuxt/app';
const router = useRouter();
const route = useRoute();

const api = new API();
var sourceData = ref(null);
var positionData = ref(null);
const request = ref([]);

let jwtToken, accountId;
if (typeof localStorage !== 'undefined') {
    jwtToken = localStorage.getItem('token');
}

if (jwtToken) {
    try {
        const decodedToken = jwtDecode(jwtToken);
        accountId = decodedToken.Id;
    } catch (error) {
        console.error('Error decoding JWT:', error);
    }
}

/**
 * Get data position and source
 */
onMounted(async () => {
    const positionAPI = await api.get('/Position');
    positionData.value = positionAPI.data.responseData;
    const sourceAPI = await api.get('/Source');
    sourceData.value = sourceAPI.data.responseData;
});

const handleFileChange = async (event) => {
    const files = event.target.files;
    const formData = new FormData();
    formData.append('pdfFile', files[0]);
    var response = await api.postAPI('/CV/PDFToText', formData);

    request.value.nameCandidate = convertToProperCase(response.data.name);
    request.value.school = response.data.school;
    request.value.positionId = response.data.position != "" ? positionData.value.filter(
        (x) => x.positionName == response.data.position,
    )[0].id : "";
    request.value.phoneNumber = response.data.phone;
    request.value.email = response.data.email;
};

const props = defineProps({
    data_bs_target_modal: {
        type: String,
        default: '',
    },
    id_model: {
        type: String,
        default: '',
    },
    idCv: {
        type: Number,
        required: true,
    },
    cvData: {
        type: Object,
        default: '',
    },
});

/**
 * Handle yyyy-MM-dd format dates
 * @param {*} date
 */
const formatDate = (date) => {
    if (typeof date !== 'string') {
        return 'Invalid Date';
    }
    const dateObj = new Date(date);
    if (isNaN(dateObj.getTime())) {
        return 'Invalid Date';
    }
    return format(dateObj, 'yyyy-MM-dd');
};

const convertToProperCase = (text) => {
    const words = text.split(' ');
    const convertedWords = words.map((word) => {
        return word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
    });
    const convertedString = convertedWords.join(' ');
    return convertedString;
};

watch(
    () => props.cvData,
    (newVal) => {
        request.value = { ...newVal };
        request.value.dateReceiveCV = formatDate(props.cvData.dateReceiveCV);
        request.value.dateOfInterview = formatDate(props.cvData.dateOfInterview);
        request.value.dateAcceptJob = formatDate(props.cvData.dateAcceptJob);
    },
    { immediate: true },
);
const emit = defineEmits(['reloadCV']);

/**
 * Handle submit form add or edit CV
 */
const submitForm = async (event) => {
    if (props.idCv == null) {
        const formData = new FormData();
        formData.append('nameCandidate', request.value.nameCandidate);
        formData.append('phoneNumber', request.value.phoneNumber);
        formData.append('school', request.value.school);
        formData.append('yearOfBirth', request.value.yearOfBirth);
        formData.append('email', request.value.email);
        formData.append('dateReceiveCV', request.value.dateReceiveCV);
        formData.append('positionId', request.value.positionId);
        formData.append('sourceId', request.value.sourceId);
        formData.append('sourceOfCV', request.value.sourceOfCV);
        formData.append('cvNote', request.value.cvNote ?? '');
        formData.append('appUserId', accountId);
        var response = await api.postAPI('/CV', formData);
        emit('reloadCV');
        $('.btn-close').click();
    } else {
        const editRequest = ref([]);
        editRequest.value = { ...request.value };
        if (request.value.dateOfInterview == 'Invalid Date') {
            delete editRequest.value.dateOfInterview;
        }
        if (request.value.dateAcceptJob == 'Invalid Date') {
            delete editRequest.value.dateAcceptJob;
        }
        // for (const pair of formData.entries()) {
        //     console.log(pair[0] + ': ' + pair[1]);
        // }
        var response = await api.putAPI('/CV/' + props.idCv, editRequest.value);
        emit('reloadCV');
        $('.btn-close').click();
    }

    const textarea = document.getElementById('autoResizeTextarea');
    const adjustTextareaHeight = () => {
        textarea.style.height = 'auto';
        textarea.style.height = `${textarea.scrollHeight}px`;
    };
    textarea.addEventListener('input', adjustTextareaHeight);
    adjustTextareaHeight();
};
</script>
<style>
/* .phone-link {
    color: black;
    text-decoration: none;

    &:hover {
        text-decoration: underline;
        color: rgb(234, 8, 15);
    }
} */

textarea.note-cv {
    min-height: 60px;
}

textarea#autoResizeTextarea {
    min-height: 60px;
}

::-webkit-scrollbar {
    width: 10px;
}
</style>
