<template>
    <div class="row">
        <div class="p-4">
            <div class="search">
                <h4 class="search-text h5">Vị trí tuyển dụng</h4>
                <form name="search" @submit.prevent="search">
                    <div class="mt-3">Tìm kiếm:</div>
                    <div class="search-text d-flex align-content-center">
                        <div class="col-md-3">
                            <input
                                class="form-control col-md-2"
                                v-model="searchString"
                                type="text"
                                placeholder=" tên... "
                            />
                        </div>
                        <span class="button d-flex">
                            <button
                                class="ms-2 btn-sm me-1 btn btn-primary"
                                type="submit"
                            >
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    width="1.5em"
                                    height="1.5em"
                                    viewBox="0 0 24 24"
                                >
                                    <path
                                        fill="currentColor"
                                        d="M15.5 14h-.79l-.28-.27A6.471 6.471 0 0 0 16 9.5A6.5 6.5 0 1 0 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5S14 7.01 14 9.5S11.99 14 9.5 14"
                                    />
                                </svg>
                                Tìm kiếm
                            </button>
                            <button
                                class="btn btn-sm btn-outline-primary"
                                type="reset"
                                @click="resetSearchForm()"
                            >
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    width="1.5em"
                                    height="1.5em"
                                    viewBox="0 0 256 256"
                                >
                                    <path
                                        fill="currentColor"
                                        d="M225 80.4L183.6 39a24 24 0 0 0-33.94 0L31 157.66a24 24 0 0 0 0 33.94l30.06 30.06a8 8 0 0 0 5.68 2.34H216a8 8 0 0 0 0-16h-84.7l93.7-93.66a24 24 0 0 0 0-33.94M213.67 103L160 156.69L107.31 104L161 50.34a8 8 0 0 1 11.32 0l41.38 41.38a8 8 0 0 1 0 11.31Z"
                                    />
                                </svg>
                                Xóa Form
                            </button>
                        </span>
                    </div>
                </form>
            </div>
            <div class="data">
                <div class="btn-handle d-flex justify-content-end mb-3">
                    <div class="handle-create">
                        <TheFormCreateUpdatePosition
                            :isEditMode="isEditMode"
                            :editPosition="editPositionData"
                            @position-saved="getPositions"
                        />
                        <a
                            @click="onCreateMode"
                            href="#"
                            data-bs-toggle="modal"
                            data-bs-target="#create-update-position-modal"
                        >
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                width="2em"
                                height="2em"
                                viewBox="0 0 256 256"
                            >
                                <path
                                    fill="currentColor"
                                    d="M208 32H48a16 16 0 0 0-16 16v160a16 16 0 0 0 16 16h160a16 16 0 0 0 16-16V48a16 16 0 0 0-16-16m-24 104h-48v48a8 8 0 0 1-16 0v-48H72a8 8 0 0 1 0-16h48V72a8 8 0 0 1 16 0v48h48a8 8 0 0 1 0 16"
                                />
                            </svg>
                        </a>
                    </div>
                    <div class="reload">
                        <a class="text-info" href="#" @click="getPositions()">
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                width="2em"
                                height="2em"
                                viewBox="0 0 512 512"
                            >
                                <path
                                    fill="currentColor"
                                    d="M256 48C141.31 48 48 141.31 48 256s93.31 208 208 208s208-93.31 208-208S370.69 48 256 48m120 190.77h-89l36.88-36.88l-5.6-6.51a87.38 87.38 0 1 0-62.94 148a87.55 87.55 0 0 0 82.42-58.25l5.37-15.13l30.17 10.67l-5.3 15.13a119.4 119.4 0 1 1-112.62-159.18a118.34 118.34 0 0 1 86.36 36.95l.56.62l4.31 5L376 149.81Z"
                                />
                            </svg>
                        </a>
                    </div>
                </div>
                <table class="table text-center table-hover">
                    <thead class="table-active table-primary">
                        <tr>
                            <th scope="col">No.</th>
                            <th scope="col">Tên vị trí tuyển dụng</th>
                            <th scope="col">Tác vụ</th>
                        </tr>
                    </thead>
                    <tbody class="table-success">
                        <tr v-for="(position, index) in positions" :key="index">
                            <td>{{ ++index }}</td>
                            <td>{{ position.positionName }}</td>
                            <td>
                                <div v-if="position.positionName.toLowerCase() !== 'unknown'"
                                    class="d-flex justify-content-center align-content-center align-items-center">
                                    <NuxtLink
                                        class="link link-primary text-decoration-none mx-1"
                                        data-bs-toggle="modal"
                                        data-bs-target="#create-update-position-modal"
                                        @click="positionEdit(position)"
                                        style="cursor: pointer"
                                    >
                                        <svg
                                            xmlns="http://www.w3.org/2000/svg"
                                            width="1em"
                                            height="1em"
                                            viewBox="0 0 24 24"
                                        >
                                            <path
                                                fill="currentColor"
                                                d="M5 18.08V19h.92l9.06-9.06l-.92-.92z"
                                                opacity="0.3"
                                            />
                                            <path
                                                fill="currentColor"
                                                d="M20.71 7.04a.996.996 0 0 0 0-1.41l-2.34-2.34c-.2-.2-.45-.29-.71-.29s-.51.1-.7.29l-1.83 1.83l3.75 3.75zM3 17.25V21h3.75L17.81 9.94l-3.75-3.75zM5.92 19H5v-.92l9.06-9.06l.92.92z"
                                            />
                                        </svg>
                                    </NuxtLink>
                                    <NuxtLink
                                        class="link link-danger text-decoration-none mx-1"
                                        style="cursor: pointer"
                                        @click="deletePosition(position.id)"
                                        data-bs-toggle="modal"
                                        data-bs-target="#deletePositionModal"
                                    >
                                        <svg
                                            xmlns="http://www.w3.org/2000/svg"
                                            width="1em"
                                            height="1em"
                                            viewBox="0 0 48 48"
                                        >
                                            <path
                                                fill="currentColor"
                                                d="M20 10.5v.5h8v-.5a4 4 0 0 0-8 0m-2.5.5v-.5a6.5 6.5 0 1 1 13 0v.5h11.25a1.25 1.25 0 1 1 0 2.5h-2.917l-2 23.856A7.25 7.25 0 0 1 29.608 44H18.392a7.25 7.25 0 0 1-7.224-6.644l-2-23.856H6.25a1.25 1.25 0 1 1 0-2.5zm4 9.25a1.25 1.25 0 1 0-2.5 0v14.5a1.25 1.25 0 1 0 2.5 0zM27.75 19c-.69 0-1.25.56-1.25 1.25v14.5a1.25 1.25 0 1 0 2.5 0v-14.5c0-.69-.56-1.25-1.25-1.25"
                                            />
                                        </svg>
                                    </NuxtLink>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <DeletePositionModal
                    :positionId="selectedPositionId"
                    @position-deleted="getPositions"
                    @hide-modal="getPositions()"
                />
                <!-- <Pagination :max="5" :page-count="5" :total="100" :model-value="1" /> -->
            </div>
        </div>
    </div>
</template>
<script setup>
import { ref } from 'vue';
import DeletePositionModal from '~/components/form/DeletePositionModal.vue';
import Api from '~/service/Base/api.ts';
const api = new Api();
const positions = ref([]);
const selectedPositionId = ref(null);
const editPositionData = ref({});
const isEditMode = ref(false);
const searchString = ref('');

/**
 * auth
 */
definePageMeta({
    middleware: 'auth',
});

/**
 * get all positions
 */
const getPositions = async () => {
    try {
        const res = await api.get(`/Position`, null);
        positions.value = res.data.responseData;
    } catch (err) {
        console.log(err);
    }
};

/**
 * delete position
 * @param {*} id
 */
const deletePosition = async (id) => {
    try {
        selectedPositionId.value = id;
    } catch (err) {
        console.log(err);
    }
};

/**
 * edit position
 * @param {*} position
 */
const positionEdit = async (position) => {
    try {
        editPositionData.value = {
            positionName: position.positionName,
            id: position.id,
        };
        isEditMode.value = true;
    } catch (err) {
        console.log(err);
    }
};

/**
 * send value to modal in create mode
 */
const onCreateMode = async () => {
    try {
        isEditMode.value = false;
        editPositionData.value = {
            Name: '',
        };
    } catch (err) {
        console.log(err);
    }
};

/**
 * reset form search
 */
const resetSearchForm = async () => {
    try {
        searchString.value = '';
        getPositions();
    } catch (err) {
        console.log(err);
    }
};

/**
 * search
 */
const search = async () => {
    try {
        if (!searchString.value) {
            getPositions();
            return;
        } else {
            const res = await api.get(
                `/Position/SearchPosition/${searchString.value}`,
                null,
            );
            positions.value = res.data.responseData;
        }
    } catch (err) {
        console.log(err);
    }
};

/**
 * call
 */
getPositions();
</script>
