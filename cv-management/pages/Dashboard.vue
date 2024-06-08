<template>
    <div class="row p-5">
        <div class="col-md-4">
            <DashboardComponent :customClass="'bg-blue'">
                <template #quantity>
                    {{ cv.length }}
                </template>
                <template #name> Tổng số CV </template>
                <template #icon>
                    <svg
                        color="white"
                        xmlns="http://www.w3.org/2000/svg"
                        width="2em"
                        height="2em"
                        viewBox="0 0 28 28"
                    >
                        <path
                            fill="currentColor"
                            d="M7.25 2A3.25 3.25 0 0 0 4 5.25v17.5A3.25 3.25 0 0 0 7.25 26h9.568l-2.157-2.157a2.25 2.25 0 1 1 3.182-3.182l1.407 1.407l4.409-4.409c.106-.106.22-.2.341-.28V5.25A3.25 3.25 0 0 0 20.75 2zm3.25 6.75a1.25 1.25 0 1 1-2.5 0a1.25 1.25 0 0 1 2.5 0m-1.25 6.5a1.25 1.25 0 1 1 0-2.5a1.25 1.25 0 0 1 0 2.5m0 5.25a1.25 1.25 0 1 1 0-2.5a1.25 1.25 0 0 1 0 2.5M12.75 8h6.5a.75.75 0 0 1 0 1.5h-6.5a.75.75 0 0 1 0-1.5M12 14a.75.75 0 0 1 .75-.75h6.5a.75.75 0 0 1 0 1.5h-6.5A.75.75 0 0 1 12 14m.75 4.5h6.5a.75.75 0 0 1 0 1.5h-6.5a.75.75 0 0 1 0-1.5m7.03 7.28l6-6a.75.75 0 1 0-1.06-1.06l-5.47 5.47l-2.468-2.468a.75.75 0 0 0-1.06 1.06l2.998 2.998a.75.75 0 0 0 1.06 0"
                        />
                    </svg>
                </template>
            </DashboardComponent>
        </div>
        <div class="col-md-4">
            <DashboardComponent :customClass="'bg-green'">
                <template #quantity>
                    {{ refused(cv) }}
                </template>
                <template #name> Ứng viên từ chối </template>
                <template #icon>
                    <svg
                        color="white"
                        xmlns="http://www.w3.org/2000/svg"
                        width="2em"
                        height="2em"
                        viewBox="0 0 24 24"
                    >
                        <path
                            fill="currentColor"
                            d="M12 3c5.5 0 10 3.58 10 8s-4.5 8-10 8c-1.24 0-2.43-.18-3.53-.5C5.55 21 2 21 2 21c2.33-2.33 2.7-3.9 2.75-4.5C3.05 15.07 2 13.13 2 11c0-4.42 4.5-8 10-8"
                        />
                    </svg>
                </template>
            </DashboardComponent>
        </div>
        <div class="col-md-4">
            <DashboardComponent :customClass="'bg-pink'">
                <template #quantity>
                    {{ backup(cv) }}
                </template>
                <template #name> Tổng số Backup </template>
                <template #icon>
                    <svg
                        color="white"
                        xmlns="http://www.w3.org/2000/svg"
                        width="2em"
                        height="2em"
                        viewBox="0 0 24 24"
                    >
                        <path
                            fill="currentColor"
                            d="m18 20l-1.4-1.4l1.575-1.6H14v-2h4.175L16.6 13.4L18 12l4 4zM4 17q-.825 0-1.412-.587T2 15V5q0-.825.588-1.412T4 3h13q.825 0 1.413.588T19 5v5.1q-.25-.05-.5-.075T18 10q-.25 0-.5.013t-.5.062V6.4L10.4 11L4 6.425V15h8.075q-.05.25-.062.5T12 16q0 .25.025.5t.075.5zM5.45 5l4.95 3.55L15.5 5zM4 15V5z"
                        />
                    </svg>
                </template>
            </DashboardComponent>
        </div>
        <div class="col-md-4">
            <DashboardComponent :customClass="'bg-dark-purple'">
                <template #quantity>
                    {{ getJob(cv) }}
                </template>
                <template #name> Nhận việc </template>
                <template #icon>
                    <svg
                        color="white"
                        xmlns="http://www.w3.org/2000/svg"
                        width="2em"
                        height="2em"
                        viewBox="0 0 24 24"
                    >
                        <path
                            fill="currentColor"
                            d="M1 18q-.425 0-.712-.288T0 17v-.575q0-1.075 1.1-1.75T4 14q.325 0 .625.013t.575.062q-.35.525-.525 1.1t-.175 1.2V18zm6 0q-.425 0-.712-.288T6 17v-.625q0-.8.438-1.463t1.237-1.162q.8-.5 1.913-.75T12 12.75q1.325 0 2.438.25t1.912.75q.8.5 1.225 1.163T18 16.375V17q0 .425-.287.713T17 18zm12.5 0v-1.625q0-.65-.162-1.225t-.488-1.075q.275-.05.563-.062T20 14q1.8 0 2.9.663t1.1 1.762V17q0 .425-.288.713T23 18zM4 13q-.825 0-1.412-.587T2 11q0-.85.588-1.425T4 9q.85 0 1.425.575T6 11q0 .825-.575 1.413T4 13m16 0q-.825 0-1.412-.587T18 11q0-.85.588-1.425T20 9q.85 0 1.425.575T22 11q0 .825-.575 1.413T20 13m-8-1q-1.25 0-2.125-.875T9 9q0-1.275.875-2.137T12 6q1.275 0 2.138.863T15 9q0 1.25-.862 2.125T12 12"
                        />
                    </svg>
                </template>
            </DashboardComponent>
        </div>
        <div class="col-md-4">
            <DashboardComponent :customClass="'bg-light-purple'">
                <template #quantity>
                    {{ passInterview(cv) }}
                </template>
                <template #name> Qua phỏng vấn </template>
                <template #icon>
                    <svg
                        color="white"
                        xmlns="http://www.w3.org/2000/svg"
                        width="2em"
                        height="2em"
                        viewBox="0 0 24 24"
                    >
                        <g
                            fill="none"
                            stroke="currentColor"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            stroke-width="2"
                        >
                            <circle cx="12" cy="12" r="9" />
                            <path d="m15 10l-4 4l-2-2" />
                        </g>
                    </svg>
                </template>
            </DashboardComponent>
        </div>
        <div class="col-md-4">
            <DashboardComponent :customClass="'bg-yellow'">
                <template #quantity>
                    {{ account }}
                </template>
                <template #name> Tài khoản </template>
                <template #icon>
                    <svg
                        color="white"
                        xmlns="http://www.w3.org/2000/svg"
                        width="2em"
                        height="2em"
                        viewBox="0 0 24 24"
                    >
                        <path
                            fill="currentColor"
                            d="M12 4a4 4 0 0 1 4 4a4 4 0 0 1-4 4a4 4 0 0 1-4-4a4 4 0 0 1 4-4m0 10c4.42 0 8 1.79 8 4v2H4v-2c0-2.21 3.58-4 8-4"
                        />
                    </svg>
                </template>
            </DashboardComponent>
        </div>
    </div>
</template>

<script setup>
import constants from '../assets/js/constants/constants.js';
import { ref, onMounted } from 'vue';
import API from '../service/Base/api';
import DashboardComponent from '../components/DashboardComponent';
definePageMeta({
    middleware: 'auth',
});

const api = new API();
const cv = ref([]);
const account = ref(0);

/**
 * Calculate quantity
 * @param {*} data
 */
const refused = (data) => {
    return data.filter(
        (item) =>
            item.status == constants.REFUSED_TO_BE_INTERVIEWED ||
            item.status == constants.REFUSE_TO_ACCEPT_JOB,
    ).length;
};
const getJob = (data) => {
    return data.filter((item) => item.status == constants.GET_JOB).length;
};
const backup = (data) => {
    return data.filter((item) => item.status == constants.INTERVIEW_RES_BACKUP).length;
};
const passInterview = (data) => {
    return data.filter((item) => item.status == constants.INTERVIEW_RES_PASS).length;
};

/**
 * Get data CV, account
 */
onMounted(async () => {
    const accountAPI = await api.get('/Account/All');
    account.value = accountAPI.data.responseData.length;
    const cvAPI = await api.get('/CV');
    cv.value = cvAPI.data.responseData;
});
</script>

<style lang="scss" scoped></style>
