<template>
    <div data-bs-toggle="modal" :data-bs-target="data_bs_target_modal" data-bs-whatever="@mdo">
        <slot name="icon"></slot>
    </div>
    <div class="modal fade" :id="id_model" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <Form ref="myForm" @submit="submitForm">
                <div class="modal-content">
                    <div class="modal-header">
                        <i class="text-primary me-1 fa-solid fa-circle-plus"></i>
                        <h5 class="modal-title text-primary" id="exampleModalLabel">
                            <slot name="header"></slot>
                        </h5>
                        <button type="button" class="btn-close" @click="resetForm" data-bs-dismiss="modal"
                            aria-label="Close"></button>
                    </div>
                    <div class="modal-body" style="text-align: left;">
                        <div class="container-kanban-item">
                            <div class="row">
                                <div class="col-lg-4 col-sm-12 mb-5">
                                    <article
                                        class="d-flex flex-column mx-3 mb-4 align-items-center justify-content-center">
                                        <Field :rules="{ required: true }"
                                            class=" text-wrap fw-semibold fs-4 text-center input-edit w-100" type="text"
                                            name="name" v-model="request.nameCandidate" id="name"
                                            placeholder="Tên ứng viên" />
                                        <ErrorMessage name="name" class="text-danger" />
                                        <span class="text-wrap mt-1">
                                            <Field as="select" required :rules="{ required: true }"
                                                :class="colorSelect(request.positionId)" class="input-edit form-select"
                                                v-model="request.positionId" aria-label="Default select example"
                                                name="job-position" id="job-position">
                                                <option value="" disabled class="text-secondary" selected>Vị trí
                                                </option>
                                                <option :rules="{ required: true }" class="text-dark" name="job-position"
                                                    v-for="position in positionData" :value="position.id">
                                                    {{ position.positionName }}
                                                </option>
                                            </Field>
                                            <ErrorMessage name="job-position" class="text-danger" />
                                        </span>
                                    </article>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <a class="phone-link" :href="`tel://${request.phoneNumber}`"><i
                                                    class="fa-solid fa-phone me-1"></i></a>
                                        </template>
                                        <template #information-cv>
                                            <Field type="text" class="input-edit w-100" name="phone-number"
                                                :rules="{ required: true, phone: true }" v-model="request.phoneNumber"
                                                id="phone-number" placeholder="Điện thoại" />
                                            <br />
                                            <ErrorMessage name="phone-number" class="text-danger" />
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-envelope"></i>
                                        </template>
                                        <template #information-cv>
                                            <Field type="email" name="email" class="input-edit w-100"
                                                :rules="{ required: true, email: true }" v-model="request.email"
                                                id="email" placeholder="Email" />
                                            <br />
                                            <ErrorMessage name="email" class="text-danger" />
                                            <span :class="getBackgroundColor(request.isSendMail)">{{
                                                getVietNameseText(request.isSendMail)
                                            }}
                                            </span>
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-cake-candles"></i>
                                        </template>
                                        <template #information-cv>
                                            <Field type="text" name="birthday" class="input-edit w-100"
                                                :rules="{ required: true }" v-model="request.yearOfBirth" id="birthday"
                                                placeholder="Năm sinh" />
                                            <br />
                                            <ErrorMessage name="birthday" class="text-danger" />
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-school me-1"></i>
                                        </template>
                                        <template #information-cv>
                                            <Field type="text" name="school" :rules="{ required: true }"
                                                class="input-edit w-100" v-model="request.school" id="school"
                                                placeholder="Trường" />
                                            <br />
                                            <ErrorMessage name="school" class="text-danger" />
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-brands fa-sourcetree"></i>
                                        </template>
                                        <template #information-cv>
                                            <Field as="select" id="source" :rules="{ required: true }" name="source"
                                                class=" input-edit" :class="colorSelect(request.sourceId)"
                                                aria-label="Default select example" v-model="request.sourceId">
                                                <option value="" disabled class="text-secondary" selected>Nguồn CV
                                                </option>
                                                <option v-for="source in sourceData" class="text-dark"
                                                    :value="source.id">
                                                    {{ source.nameSource }}
                                                </option>
                                            </Field>
                                            <br />
                                            <ErrorMessage name="source" class="text-danger" />
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>

                                            <a :href="request.sourceOfCV" class="phone-link" target="_blank">
                                                <i class="fa-solid fa-globe me-1"></i>
                                            </a>
                                        </template>
                                        <template #information-cv>
                                            <span>
                                                <Field type="text" class="input-edit w-100" name="link-source"
                                                    :rules="{ required: true }" id="link-source"
                                                    v-model="request.sourceOfCV" placeholder="Link CV" />
                                                <br>
                                                <ErrorMessage name="link-source" class="text-danger" />
                                            </span>
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-calendar me-1"></i>
                                        </template>
                                        <template #information-cv>
                                            <span class="me-1">Ngày nhận CV: </span>
                                            <span>
                                                <input type="date" class="input-edit me-1"
                                                    v-model="request.dateReceiveCV" />
                                            </span>
                                        </template>
                                    </the-kanban-item-text>
                                    <div v-if="props.idCv != null">
                                        <the-kanban-item-text customStyle="border-bottom">
                                            <template #icon>
                                                <i class="fa-solid fa-chart-bar"></i>
                                            </template>
                                            <template #information-cv>
                                                <select id="status-review" class="input-edit "
                                                    aria-label="Default select example" v-model="request.status">
                                                    <option v-for="constant in CV_CONSTANT.Search" :value="constant">
                                                        {{ constant }}
                                                    </option>
                                                </select>
                                            </template>
                                        </the-kanban-item-text>
                                        <the-kanban-item-text customStyle="border-bottom">
                                            <template #icon>
                                                <i class="fa-solid fa-star"></i>
                                            </template>
                                            <template #information-cv>
                                                <Field class="input-edit " :rules="{ point: true }" id="rate"
                                                    name="rate" v-model="request.rate" placeholder="Điểm" />
                                                <br />
                                                <ErrorMessage name="rate" class="text-danger" />
                                            </template>
                                        </the-kanban-item-text>
                                        <section
                                            v-if="(request.status === CV_CONSTANT.REVIEW_CV_PASS) || (request.status === CV_CONSTANT.INTERVIEW_RES_PASS)">
                                            <the-kanban-item-text customStyle="border-bottom">
                                                <template #icon>
                                                    <i class="fa-solid fa-calendar me-1"></i>
                                                </template>
                                                <template #information-cv>
                                                    <span class="me-1">Ngày PV: </span>
                                                    <span>
                                                        <input type="time" class="input-edit me-1"
                                                            v-model="request.timeOfInterview" />
                                                        <input type="date" class="input-edit "
                                                            v-model="request.dateOfInterview" />
                                                    </span>
                                                </template>
                                            </the-kanban-item-text>
                                        </section>
                                        <section v-if="request.status === CV_CONSTANT.INTERVIEW_RES_PASS">
                                            <the-kanban-item-text customStyle="border-bottom">
                                                <template #icon>
                                                    <i class="fa-solid fa-calendar me-1"></i>
                                                </template>
                                                <template #information-cv>
                                                    <span class="label-item-text">Ngày làm: </span>
                                                    <input type="date" class="input-edit " id="date-start-work"
                                                        v-model="request.dateAcceptJob" />
                                                </template>
                                            </the-kanban-item-text>
                                        </section>
                                    </div>
                                    <div v-if="props.idCv == null">
                                        <the-kanban-item-text customStyle="border-bottom">
                                            <template #icon>
                                                <i class="fa-solid fa-file-import"></i>
                                            </template>
                                            <template #information-cv>
                                                <input id="fileInput" accept=".pdf" type="file"
                                                    @change="handleFileChange" />
                                            </template>
                                        </the-kanban-item-text>
                                    </div>
                                </div>
                                <div class="col-lg-8 col-sm-12 border-start pe-2"
                                    style="max-height: 500px; overflow: scroll;">
                                    <section class="interview-assessments ms-3 mb-4 border-bottom">
                                        <h5 class="mb-0 ">
                                            <i class="fa-solid fa-note-sticky"></i>
                                            Note
                                        </h5>
                                        <TipTap v-model="request.cvNote"></TipTap>
                                        <!-- <input type="text" class="input-edit "
                                                        v-model="request.cvNote" /> -->
                                    </section>
                                    <div v-if="props.idCv != null">
                                        <section class="interview-assessments ms-3 mb-4 border-bottom">
                                            <h5 class="mb-0 ">
                                                <i class="fa-solid fa-note-sticky"></i>
                                                Review CV
                                            </h5>
                                            <TipTap v-model="request.reviewCV"></TipTap>
                                        </section>
                                        <section class="interview-assessments ms-3 mb-4 border-bottom">
                                            <h5 class="mb-0 ">
                                                <i class="fa-solid fa-gavel"></i>
                                                Đánh giá
                                            </h5>
                                            <TipTap v-model="request.cvEvaluate"></TipTap>
                                        </section>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" @click="resetForm" class="btn btn-secondary"
                            data-bs-dismiss="modal">Đóng</button>
                        <button type="submit" class="btn btn-primary">
                            <span v-if="props.idCv == null">Thêm</span>
                            <span v-else>Cập nhật</span>
                        </button>
                    </div>
                </div>
            </Form>
        </div>
    </div>
</template>
<script setup>
import { ref } from 'vue';
import API from '../../service/Base/api.ts';
import { format } from 'date-fns';
import { Form, Field, ErrorMessage } from 'vee-validate';
import { jwtDecode } from 'jwt-decode';
import CV_CONSTANT from '~/assets/js/constants/constants.js';
import { getBackgroundColor, getVietNameseText } from '~/assets/js/helper.js';

const api = new API();
var sourceData = ref([]);
var positionData = ref([]);
const request = ref([]);
const emit = defineEmits(['reloadCV']);
const myForm = ref(null);
let jwtToken, accountId;
const originalStatus = ref(null)

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
    }
});

const checkDateChange = () => {
    if (request.value.status !== originalStatus.value) {
        alert('Giá trị đã thay đổi.');
    } else {
        alert('Giá trị không thay đổi.');
    }
}

/**
 * Handle color in form select
 * @param {*} id
 * @returns string
 */
const colorSelect = (id) => {
    if (id == null)
        return "text-secondary"
    return ""
}

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

/**
 * Track input changes to handle api calls PDFToText and UploadPDF
 * @param {*} event
 */
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
    request.value.yearOfBirth = response.data.birthYear;

    var pdfResponse = await api.postAPI('/CV/UploadPDF', formData);
    request.value.sourceOfCV = pdfResponse.data;
};

/**
 * Convert string to Proper Case
 * @param {*} text
 */
const convertToProperCase = (text) => {
    const words = text.split(' ');
    const convertedWords = words.map((word) => {
        return word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
    });
    const convertedString = convertedWords.join(' ');
    return convertedString;
};

/**
 * Watch for cvData props changes and update request.value again
 */
watch(
    () => props.cvData,
    (newVal) => {
        request.value = { ...newVal };
        request.value.dateReceiveCV = formatDate(props.cvData.dateReceiveCV);
        request.value.dateOfInterview = formatDate(props.cvData.dateOfInterview);
        request.value.dateAcceptJob = formatDate(props.cvData.dateAcceptJob);
        originalStatus.value = props.cvData.status
    },
    { immediate: true },
);

/**
 * Hanlde reset form input
 */
const resetForm = () => {
    if (myForm.value) {
        myForm.value.resetForm();

    }
    request.value.cvNote = '';
    var fileInput = document.getElementById('fileInput');
    fileInput.value = '';
    request.value.dateReceiveCV = '';
    if(props.idCv != null){
        request.value = props.cvData
    }
};

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
        if (request.value.dateReceiveCV != 'Invalid Date') {
            formData.append('dateReceiveCV', request.value.dateReceiveCV);
        }
        formData.append('positionId', request.value.positionId);
        formData.append('sourceId', request.value.sourceId);
        formData.append('sourceOfCV', request.value.sourceOfCV);
        formData.append('cvNote', request.value.cvNote ?? '');
        formData.append('appUserId', accountId);
        // for (const pair of formData.entries()) {
        //     console.log(pair[0] + ': ' + pair[1]);
        // }
        var response = await api.postAPI('/CV', formData);
        if (Math.floor(response.data.statusCode / 100) == 2) {
            emit('reloadCV');
            $('.btn-close').click();
            resetForm()
        }
    } else {
        const editRequest = ref([]);
        editRequest.value = { ...request.value };
        if (request.value.dateReceiveCV == 'Invalid Date')
            delete editRequest.value.dateReceiveCV;
        if (request.value.dateOfInterview == 'Invalid Date')
            delete editRequest.value.dateOfInterview;
        if (request.value.dateAcceptJob == 'Invalid Date')
            delete editRequest.value.dateAcceptJob;
        if (request.value.status !== originalStatus.value) {
            editRequest.value.isSendMail = CV_CONSTANT.NOT_SENT;
        }
        var response = await api.putAPI('/CV/' + props.idCv, editRequest.value);
        if (Math.floor(response.data.statusCode / 100) == 2) {
            emit('reloadCV');
            $('.btn-close').click();
        }
        // checkDateChange()
    }
};
</script>
<style>
.phone-link {
    color: black;
    text-decoration: none;

    &:hover {
        text-decoration: underline;
        color: rgb(0, 77, 221);
        cursor: pointer;
    }
}

.label-item-text {
    display: inline-block;
    min-width: 80px;
}

input.input-edit,
select.input-edit {
    border: none;
}

input.input-edit:focus,
select.input-edit:focus {
    border-color: #86b7fe !important;
    outline: 0 !important;
    box-shadow: 0 0 0 2px rgba(9, 29, 57, 0.681) !important;
}

select {
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
}

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
