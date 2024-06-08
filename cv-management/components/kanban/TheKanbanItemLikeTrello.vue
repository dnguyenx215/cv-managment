<script setup>
import { ref, defineEmits } from 'vue';
import axios from 'axios';
import Api from '~/service/Base/api.ts';
import { jwtDecode } from 'jwt-decode';
import CV_CONSTANT from '~/assets/js/constants/constants.js';
import { apiFactory } from '~/plugins/api';
import { getBackgroundColor, getVietNameseText, formatDate, formatDateYYYYmmdd } from '~/assets/js/helper.js';
import { ErrorMessage } from 'vee-validate';
const emit = defineEmits(['getEmailTemplate'])
const api = new Api();
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
    name: {
        type: String,
        default: 'true',
    },
});

/**
 * Handle submit form add or edit CV
 */
const positionData = ref([]);

const submitForm = async (event) => {

    if (true) {

        var updateCvNote = props.cvData;
        var response = await apiFactory.cv.updateCV(updateCvNote.id, updateCvNote);

        if (Math.floor(response.data.statusCode / 100) == 2) {
            $('.btn-close').click();
            emit('getEmailTemplate')
        }
    } else {
        const formData = new FormData();
        formData.append('id', 'aaa');
        // var response = await api.putAPI('/CV/PutCV/' + props.idCv, formData);
        $('.btn-close').click();
    }
};

/**
 * Watch for cvData props changes and update request.value again
 */
watch(
    () => props.cvData,
    () => {
        // if (props.cvData.dateOfInterview) {
        //     interviewDate.value = formatDateYYYYmmdd(props.cvData.dateOfInterview);
        // }
        // if (props.cvData.dateAcceptJob) {
        //     acceptJobDate.value = formatDateYYYYmmdd(props.cvData.dateAcceptJob);
        // }
    },
    { immediate: true },
);

onMounted(async () => {
    // const sourceAPI = await api.get('/Source');
    // sourceData.value = sourceAPI.data.responseData;
    // const model = document.querySelector(`#update${props.name}`);
    // model.addEventListener('pointerdown', (e) => {
    //     e.stopPropagation();
    // });
    const positionAPI = await api.get('/Position');
    positionData.value = positionAPI.data.responseData;


});

</script>

<template>
    <div data-bs-toggle="modal" :data-bs-target="data_bs_target_modal" data-bs-whatever="@mdo">
        <slot name="icon"></slot>
    </div>
    <div class="modal fade" :id="id_model" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <Form @submit="submitForm">
                <div class="modal-content">
                    <div class="modal-header">
                        <i class="fa-solid fa-circle-plus me-1" style="color: #74C0FC;"></i>
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Thông tin ứng viên</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container-kanban-item">
                            <div class="row">
                                <div class="col-lg-4 col-sm-12">
                                    <div class="text-center avatar mb-3">
                                        <span class=" text-wrap fw-semibold fs-3 text-center" type="text" name="name"
                                            id="name">
                                            {{ cvData.nameCandidate }}
                                        </span>
                                        <br>
                                        <span v-for="position in positionData"
                                            class="badge text-bg-light text-wrap fs-5 mt-1">
                                            {{ position.id == cvData.positionId ? position.positionName : '' }}
                                        </span>
                                    </div>

                                    <the-kanban-item-text>
                                        <template #icon>
                                            <a class="phone-link" :href="`tel://${cvData.phoneNumber}`"><i
                                                    class="fa-solid fa-phone me-1"></i></a>
                                        </template>
                                        <template #information-cv>
                                            <span class="phone-link">
                                                <a class="phone-link" :href="`tel://${cvData.phoneNumber}`">

                                                    {{ cvData.phoneNumber }}
                                                </a>
                                            </span>
                                        </template>
                                    </the-kanban-item-text>

                                    <the-kanban-item-text>
                                        <template #icon>
                                            <i class="fa-solid fa-square-envelope me-1"></i>
                                        </template>
                                        <template #information-cv>
                                            <span>{{ cvData.email }}</span>
                                            <span class="mx-2" :class="getBackgroundColor(cvData.isSendMail)">{{
                                                getVietNameseText(cvData.isSendMail)
                                            }}
                                            </span>
                                        </template>
                                    </the-kanban-item-text>

                                    <the-kanban-item-text>
                                        <template #icon>
                                            <i class="fa-solid fa-globe me-1"></i>
                                        </template>
                                        <template #information-cv>
                                            <span><a :href="cvData.sourceOfCV"
                                                    class="nav-link control-label d-inline link-info"
                                                    target="_blank">Link
                                                    CV</a></span>
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-cake-candles"></i>
                                        </template>
                                        <template #information-cv>
                                            <span>Năm sinh: </span>
                                            <span class="">
                                                {{ cvData.yearOfBirth }}
                                            </span>
                                        </template>
                                    </the-kanban-item-text>
                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-school me-1"></i>
                                        </template>
                                        <template #information-cv>
                                            <span> {{ cvData.school }}</span>
                                        </template>
                                    </the-kanban-item-text>

                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-calendar me-1"></i>
                                        </template>
                                        <template #information-cv>
                                            <span class="me-1">Ngày nhận CV: </span>
                                            <span>{{ formatDate(cvData.dateReceiveCV) }}</span>
                                        </template>
                                    </the-kanban-item-text>

                                    <the-kanban-item-text customStyle="border-bottom">
                                        <template #icon>
                                            <i class="fa-solid fa-chart-bar"></i>
                                        </template>
                                        <template #information-cv>
                                            <span class="label-item-text">{{ cvData.status }} </span>
                                        </template>
                                    </the-kanban-item-text>

                                    <section v-if="(cvData.status == CV_CONSTANT.REVIEW_CV_PASS)">
                                        <the-kanban-item-text customStyle="border-bottom">
                                            <template #icon>
                                                <i class="fa-solid fa-calendar me-1"></i>
                                            </template>
                                            <template #information-cv>
                                                <span class="me-1">Ngày PV: </span>
                                                <span>
                                                    <input type="time" class="input-edit me-1"
                                                        v-model="cvData.timeOfInterview" />
                                                    <input type="date" class="input-edit "
                                                        v-model="cvData.dateOfInterview" />
                                                </span>
                                            </template>
                                        </the-kanban-item-text>
                                    </section>

                                    <section
                                        v-if="(cvData.status == CV_CONSTANT.INTERVIEW_RES_PASS) || (cvData.status == CV_CONSTANT.INTERVIEW_RES_FAIL)">
                                        <the-kanban-item-text customStyle="border-bottom">
                                            <template #icon>
                                                <i class="fa-solid fa-calendar me-1"></i>
                                            </template>
                                            <template #information-cv>
                                                <span class="me-1">Ngày PV: </span>
                                                <span>
                                                    {{ cvData.timeOfInterview }}
                                                    {{ formatDate(cvData.dateOfInterview) }}
                                                </span>
                                            </template>
                                        </the-kanban-item-text>
                                    </section>

                                    <section v-if="cvData.status == CV_CONSTANT.INTERVIEW_RES_PASS">
                                        <the-kanban-item-text customStyle="border-bottom">
                                            <template #icon>
                                                <i class="fa-solid fa-calendar me-1"></i>
                                            </template>
                                            <template #information-cv>
                                                <span class="label-item-text">Ngày làm: </span>
                                                <input type="date" class="input-edit " id="date-start-work"
                                                    v-model="cvData.dateAcceptJob" />
                                            </template>
                                        </the-kanban-item-text>
                                    </section>
                                    <section
                                        v-if="(cvData.status == CV_CONSTANT.INTERVIEW_RES_FAIL) || cvData.status == CV_CONSTANT.INTERVIEW_RES_PASS">
                                        <the-kanban-item-text customStyle="border-bottom">
                                            <template #icon>
                                                <i class="fa-solid fa-star me-1"></i>
                                            </template>
                                            <template #information-cv>
                                                <span class="label-item-text me-1">Điểm đánh giá: </span>
                                                <Field id="rate" name="rate" :rules="{ point: true }"
                                                    class="w-25 input-edit" v-model="cvData.rate" />
                                                <ErrorMessage name="rate" class="text-danger" />
                                            </template>
                                        </the-kanban-item-text>
                                    </section>

                                </div>
                                <div class="col-lg-8 col-sm-12 mt-sm-3 border-start pe-2"
                                    style="max-height: 500px; overflow: scroll;">
                                    <section class="interview-assessments mb-4 border-bottom">
                                        <h5 class="mb-0 ">
                                            <i class="fa-solid fa-note-sticky"></i>
                                            Note
                                        </h5>
                                        <TipTap v-model="cvData.cvNote"></TipTap>
                                    </section>
                                    <div v-if="props.idCv != null">
                                        <section class="interview-assessments mb-4 border-bottom">
                                            <h5 class="mb-0 ">
                                                <i class="fa-solid fa-note-sticky"></i>
                                                Review CV
                                            </h5>
                                            <TipTap v-model="cvData.reviewCV"></TipTap>
                                        </section>
                                        <section class="interview-assessments">
                                            <h5 class="mb-0 ">
                                                <i class="fa-solid fa-gavel"></i>
                                                Đánh giá
                                            </h5>
                                            <TipTap v-model="cvData.cvEvaluate"></TipTap>
                                        </section>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Đóng</button>
                        <button type="submit" class="btn btn-primary">Cập nhật</button>
                    </div>
                </div>
            </Form>
        </div>
    </div>
</template>
<style scoped>
.label-item-text {
    display: inline-block;
    min-width: 80px;
}

.modal {
    cursor: default;
}
</style>
