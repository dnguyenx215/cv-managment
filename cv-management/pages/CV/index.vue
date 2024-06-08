<template>
    <div class="row">
        <div class="p-4">
            <div class="search">
                <h4 class="search-text h5">Tìm kiếm</h4>
                <form action="" @submit.prevent="submitForm">
                    <div class="status d-flex align-items-center">
                        <span style="width: 7%;">Trạng thái: </span>
                        <div>
                            <select class="form-select" v-model="search.status">
                                <option v-for="constant in constants.Search" class="text-dark" :value="constant">
                                    {{ constant }}
                                </option>
                            </select>
                        </div>
                    </div>
                    <div class="isSendMail mt-2">
                        <span>Gửi Mail:</span>
                        <input class="me-1" type="radio" name="isSendMail" :value="constants.NOT_SENT"
                            v-model="search.isSendMail" id="notsend" style="margin-left: 25px" />
                        <label for="notsend">Chưa gửi</label>
                        <input class="me-1 ms-3" type="radio" name="isSendMail" :value="constants.SENDING"
                            v-model="search.isSendMail" id="sending" />
                        <label for="sending">Đang gửi</label>
                        <input class="me-1 ms-3" type="radio" name="isSendMail" :value="constants.SENT"
                            v-model="search.isSendMail" id="sent" />
                        <label for="sent">Đã gửi</label>
                    </div>
                    <div class="mt-2">Tìm kiếm:</div>
                    <div class="search-text row">
                        <div class="d-flex col-md-6 mb-2">
                            <div class="col-md-4">
                                <input class="form-control" type="text" v-model="search.name" placeholder="Nhập tên" />
                            </div>
                            <div class="col-md-4 px-2">
                                <input class="form-control" type="text" v-model="search.phone" placeholder="Nhập SDT" />
                            </div>
                            <div class="col-md-4">
                                <input class="form-control" type="text" v-model="search.email"
                                    placeholder="Nhập email" />
                            </div>
                        </div>
                        <span class="button d-flex col-md-6 ps-1 mb-2">
                            <button class="ms-2 me-1 btn btn-primary" type="submit">
                                <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24">
                                    <path fill="currentColor"
                                        d="M15.5 14h-.79l-.28-.27A6.471 6.471 0 0 0 16 9.5A6.5 6.5 0 1 0 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5S14 7.01 14 9.5S11.99 14 9.5 14" />
                                </svg>Tìm kiếm
                            </button>
                            <button class="btn btn-outline-primary" type="button" @click="clearForm()">
                                <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 256 256">
                                    <path fill="currentColor"
                                        d="M225 80.4L183.6 39a24 24 0 0 0-33.94 0L31 157.66a24 24 0 0 0 0 33.94l30.06 30.06a8 8 0 0 0 5.68 2.34H216a8 8 0 0 0 0-16h-84.7l93.7-93.66a24 24 0 0 0 0-33.94M213.67 103L160 156.69L107.31 104L161 50.34a8 8 0 0 1 11.32 0l41.38 41.38a8 8 0 0 1 0 11.31Z" />
                                </svg>
                                Xóa Form
                            </button>
                        </span>
                    </div>
                </form>
            </div>
            <div class="data">
                <div class="btn-handle d-flex justify-content-end mb-3">
                    <div class="handle-excel" @click="excelExport">
                        <a class="text-success" href="#"><svg xmlns="http://www.w3.org/2000/svg" width="1em"
                                height="1em" viewBox="0 0 24 24">
                                <path fill="currentColor"
                                    d="m13.2 12l2.8 4h-2.4L12 13.714L10.4 16H8l2.8-4L8 8h2.4l1.6 2.286L13.6 8H15V4H5v16h14V8h-3zM3 2.992C3 2.444 3.447 2 3.999 2H16l5 5v13.993A1 1 0 0 1 20.007 22H3.993A1 1 0 0 1 3 21.008z" />
                            </svg></a>
                    </div>
                    <div class="handle-create">
                        <FormCreateUpdateCV data_bs_target_modal="#create" id_model="create"
                            @reloadCV="getAllPagination">
                            <template #icon>
                                <a href="#"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em"
                                        viewBox="0 0 256 256">
                                        <path fill="currentColor"
                                            d="M208 32H48a16 16 0 0 0-16 16v160a16 16 0 0 0 16 16h160a16 16 0 0 0 16-16V48a16 16 0 0 0-16-16m-24 104h-48v48a8 8 0 0 1-16 0v-48H72a8 8 0 0 1 0-16h48V72a8 8 0 0 1 16 0v48h48a8 8 0 0 1 0 16" />
                                    </svg></a>
                            </template>
                            <template #header>Thêm CV</template>
                        </FormCreateUpdateCV>
                    </div>
                    <div class="reload" @click="getAllPagination">
                        <a class="text-info" href="#"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em"
                                viewBox="0 0 512 512">
                                <path fill="currentColor"
                                    d="M256 48C141.31 48 48 141.31 48 256s93.31 208 208 208s208-93.31 208-208S370.69 48 256 48m120 190.77h-89l36.88-36.88l-5.6-6.51a87.38 87.38 0 1 0-62.94 148a87.55 87.55 0 0 0 82.42-58.25l5.37-15.13l30.17 10.67l-5.3 15.13a119.4 119.4 0 1 1-112.62-159.18a118.34 118.34 0 0 1 86.36 36.95l.56.62l4.31 5L376 149.81Z" />
                            </svg></a>
                    </div>
                </div>
                <table class="table text-center table-hover table-success table-bordered table-striped-columns">
                    <thead class="table-primary table-active">
                        <tr>
                            <th scope="col">No.</th>
                            <th colspan="1">Ứng viên</th>
                            <th colspan="2">Thông tin liên lạc</th>
                            <th colspan="2">Nhận CV</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Tác vụ</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in data" @click="handleRowClick(item.id)">
                            <th scope="row" class="p-4">{{ index + 1 }}</th>
                            <td colspan="1" class="p-4">
                                {{ item.nameCandidate }} -  <span v-for="position in positionData">
                                    <span v-if="position.id == item.positionId" class="text-danger mt-2">
                                        {{ position.positionName }}
                                    </span>
                                </span><br />
                               
                                {{ item.school }}
                            </td>
                            <td colspan="2" class="p-4">
                                <a class="phone-link" :href="`tel://${item.phoneNumber}`">{{ item.phoneNumber }}
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                        class="bi bi-telephone-forward-fill ms-2" viewBox="0 0 16 16">
                                        <path fill-rule="evenodd"
                                            d="M1.885.511a1.745 1.745 0 0 1 2.61.163L6.29 2.98c.329.423.445.974.315 1.494l-.547 2.19a.68.68 0 0 0 .178.643l2.457 2.457a.68.68 0 0 0 .644.178l2.189-.547a1.75 1.75 0 0 1 1.494.315l2.306 1.794c.829.645.905 1.87.163 2.611l-1.034 1.034c-.74.74-1.846 1.065-2.877.702a18.6 18.6 0 0 1-7.01-4.42 18.6 18.6 0 0 1-4.42-7.009c-.362-1.03-.037-2.137.703-2.877zm10.761.135a.5.5 0 0 1 .708 0l2.5 2.5a.5.5 0 0 1 0 .708l-2.5 2.5a.5.5 0 0 1-.708-.708L14.293 4H9.5a.5.5 0 0 1 0-1h4.793l-1.647-1.646a.5.5 0 0 1 0-.708" />
                                    </svg></a>
                                <br />
                                {{ item.email }}
                            </td>
                            <td colspan="2" class="p-4">
                                {{ formatDate(item.dateReceiveCV) }} <br />
                                <span v-for="source in sourceData">
                                    <span v-if="source.id == item.sourceId">
                                        <a class="sourceLink" target="_blank" :href="item.sourceOfCV">{{
                                            source.nameSource }}</a>
                                    </span>
                                </span>
                            </td>
                            <td class="p-4">{{ getVietNameseText(item.status) }}</td>
                            <td class="p-4">
                                <div class="d-flex ms-3">
                                    <FormCreateUpdateCV :idCv="selectedId" :cvData="item"
                                        :data_bs_target_modal="'#updatecv' + index" :id_model="'updatecv' + index"
                                        @reloadCV="getAllPagination">
                                        <template #icon>
                                            <a href="#"><svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em"
                                                    viewBox="0 0 24 24">
                                                    <path fill="currentColor" d="M5 18.08V19h.92l9.06-9.06l-.92-.92z"
                                                        opacity="0.3" />
                                                    <path fill="currentColor"
                                                        d="M20.71 7.04a.996.996 0 0 0 0-1.41l-2.34-2.34c-.2-.2-.45-.29-.71-.29s-.51.1-.7.29l-1.83 1.83l3.75 3.75zM3 17.25V21h3.75L17.81 9.94l-3.75-3.75zM5.92 19H5v-.92l9.06-9.06l.92.92z" />
                                                </svg></a>
                                        </template>
                                        <template #header>Sửa CV</template>
                                    </FormCreateUpdateCV>
                                    <NuxtLink style="cursor: pointer" class="text-danger" data-bs-toggle="modal"
                                        data-bs-target="#deleteCVModal">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em"
                                            viewBox="0 0 48 48">
                                            <path fill="currentColor"
                                                d="M20 10.5v.5h8v-.5a4 4 0 0 0-8 0m-2.5.5v-.5a6.5 6.5 0 1 1 13 0v.5h11.25a1.25 1.25 0 1 1 0 2.5h-2.917l-2 23.856A7.25 7.25 0 0 1 29.608 44H18.392a7.25 7.25 0 0 1-7.224-6.644l-2-23.856H6.25a1.25 1.25 0 1 1 0-2.5zm4 9.25a1.25 1.25 0 1 0-2.5 0v14.5a1.25 1.25 0 1 0 2.5 0zM27.75 19c-.69 0-1.25.56-1.25 1.25v14.5a1.25 1.25 0 1 0 2.5 0v-14.5c0-.69-.56-1.25-1.25-1.25" />
                                        </svg>
                                    </NuxtLink>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <DeleteCVModal :idCv="selectedId" @reloadCV="getAllPagination" />
                <div class="d-flex justify-content-between">
                    <div class="d-flex align-items-center">
                        <span class="me-5">Tổng số: {{ totalCount }} CV</span>
                        <span>
                            <Pagination :current-page="pageNumber" :total-pages="totalPage" :next-page="nextPage"
                                :previous-page="previousPage" :set-page="setPage" />
                        </span>
                    </div>
                    <div></div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import FormCreateUpdateCV from '../../components/form/FormCreateUpdateCV.vue';
import DeleteCVModal from '../../components/form/DeleteCVModal.vue';
import { ref, onMounted } from 'vue';
import { format } from 'date-fns';
import API from '../../service/Base/api';
import Pagination from '../../components/Pagination';
import constants from '../../assets/js/constants/constants.js';
import { getVietNameseText } from '~/assets/js/helper.js';
const api = new API();
const data = ref(null);
const positionData = ref(null);
const sourceData = ref(null);
const selectedId = ref(null);
const search = ref({
    status: '',
    isSendMail: '',
    name: '',
    phone: '',
    email: '',
});
const pageNumber = ref(1);
const totalPage = ref(null);
const totalCount = ref(null);
const pageSize = ref(6);

/**
 * auth
 */
definePageMeta({
    middleware: 'auth',
});

/**
 * Get data position, source, CV
 */
onMounted(async () => {
    const positionAPI = await api.get('/Position');
    positionData.value = positionAPI.data.responseData;
    const sourceAPI = await api.get('/Source');
    sourceData.value = sourceAPI.data.responseData;
    await getAllPagination();
});

/**
 * Handle yyyy-MM-dd format dates
 * @param {*} date
 */
const formatDate = (date) => {
    return format(new Date(date), 'dd-MM-yyyy');
};

/**
 * Get data CV
 */
const getAllPagination = async () => {
    var response = await api.get(
        `/CV/GetAllPagination?PageSize=${pageSize.value}&PageNumber=${pageNumber.value}`,
    );
    data.value = response.data.responseData.data;
    totalCount.value = response.data.responseData.pagination.totalCount;
    totalPage.value = response.data.responseData.pagination.totalPage;
};

/**
 * Handle Pagination
 */
const nextPage = () => {
    pageNumber.value++;
    getAllPagination();
};
const previousPage = () => {
    pageNumber.value--;
    getAllPagination();
};
const setPage = (number) => {
    pageNumber.value = number;
    getAllPagination();
};

/**
 * Hanlde reload data CV
 */
const reload = async () => {
    getAllPagination();
};

/**
 * Export CV data into Excel
 */
const excelExport = async () => {
    let options = {
        responseType: 'blob',
    };
    const queryParams = [];
    if (search.value.status)
        queryParams.push(`status=${search.value.status}`);
    if (search.value.isSendMail)
        queryParams.push(`isSendMail=${search.value.isSendMail}`);
    if (search.value.name)
        queryParams.push(`name=${search.value.name}`);
    if (search.value.phone)
        queryParams.push(`phone=${search.value.phone}`);
    if (search.value.email)
        queryParams.push(`email=${search.value.email}`);
    const queryString = queryParams.join('&');
    const apiUrl = `/CV/ExcelExport?${queryString}`;
    var response = await api.get(apiUrl, options);
    var blob = new Blob([response.data], {
        type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.setAttribute('download', 'dataCV.xlsx');
    document.body.appendChild(link);
    link.click();
};

/**
 * Handle get id CV
 */
const handleRowClick = (id) => {
    selectedId.value = id;
};

/**
 * Handle CV search by status, name, phone number, email
 */
const submitForm = async () => {
    const formData = new FormData();
    formData.append('status', search.value.status);
    formData.append('isSendMail', search.value.isSendMail);
    formData.append('name', search.value.name);
    formData.append('phone', search.value.phone);
    formData.append('email', search.value.email);

    if (
        search.value.status == '' &&
        search.value.isSendMail == '' &&
        search.value.name == '' &&
        search.value.phone == '' &&
        search.value.email == ''
    )
        getAllPagination();
    else {
        var response = await api.postAPI('/CV/Search', formData);
        data.value = response.data.responseData;
    }
};

/**
 * Handle clear form input
 */
const clearForm = () => {
    search.value.status = '';
    search.value.isSendMail = '';
    search.value.name = '';
    search.value.phone = '';
    search.value.email = '';
};
</script>
<style lang="scss" scoped>
.status span {
    display: inline-block;
}

.sourceLink,
.phone-link {
    color: black;
    text-decoration: none;

    &:hover {
        text-decoration: underline;
        color: rgb(234, 8, 15);
    }
}

.btn-handle svg {
    width: 30px;
    height: 30px;
}
</style>
