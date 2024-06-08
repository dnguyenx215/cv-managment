<template>
    <div class="row header">
        <div class="container-fluid row">
            <div class="col-lg-3 d-flex flex-row">
                <div class="col-lg-10 text-primary fs-4 mt-3 text-center">
                    The Adminitriator
                </div>
                <div class="col-lg-2 text-center"></div>
            </div>
            <div class="col-lg-6"></div>
            <div class="col-lg-3 d-flex flex-row mt-3">
                <div class="col-lg-6 text-black fs-5 Admin">{{ fullName }}</div>
                <div class="col-lg-1">
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        class="bi bi-person-circle"
                        viewBox="0 0 16 16"
                    >
                        <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                        <path
                            fill-rule="evenodd"
                            d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1"
                        />
                    </svg>
                </div>
                <div class="col-lg-1">
                    <div class="dropdown">
                        <div class="dropbtn">
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                width="16"
                                height="16"
                                fill="currentColor"
                                class="bi bi-caret-down-fill"
                                viewBox="0 0 16 16"
                            >
                                <path
                                    d="M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z"
                                />
                            </svg>
                        </div>
                        <div class="dropdown-content">
                            <div class="d-flex flex-row">
                                <div>
                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        width="35"
                                        height="35"
                                        fill="currentColor"
                                        class="bi bi-person pt-2"
                                        viewBox="0 0 16 16"
                                    >
                                        <path
                                            d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6m2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0m4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4m-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10s-3.516.68-4.168 1.332c-.678.678-.83 1.418-.832 1.664z"
                                        />
                                    </svg>
                                </div>
                                <div class="pt-0">
                                    <a href="#">Profile</a>
                                </div>
                            </div>
                            <div class="d-flex flex-row">
                                <div>
                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        width="35"
                                        height="35"
                                        fill="currentColor"
                                        class="bi bi-box-arrow-right pt-2"
                                        viewBox="0 0 16 16"
                                    >
                                        <path
                                            fill-rule="evenodd"
                                            d="M10 12.5a.5.5 0 0 1-.5.5h-8a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h8a.5.5 0 0 1 .5.5v2a.5.5 0 0 0 1 0v-2A1.5 1.5 0 0 0 9.5 2h-8A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h8a1.5 1.5 0 0 0 1.5-1.5v-2a.5.5 0 0 0-1 0z"
                                        />
                                        <path
                                            fill-rule="evenodd"
                                            d="M15.854 8.354a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L14.293 7.5H5.5a.5.5 0 0 0 0 1h8.793l-2.147 2.146a.5.5 0 0 0 .708.708z"
                                        />
                                    </svg>
                                </div>
                                <div class="pt-0" @click="Logout">
                                    <a href="#">Logout</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
import { ref } from 'vue';
import axios from 'axios';

const fullName = ref('');

window.addEventListener('authenticated', (event) => {
    fullName.value = event.detail.fullName;
});

const Logout = async () => {
    const res = await axios.post('https://localhost:7081/api/Account/logout');
    navigateTo('/');
};
</script>
