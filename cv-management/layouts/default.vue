<template>
    <div class="container-fluid">
        <div class="row  pt-3 pb-2">
            <div class="col-lg-3 ms-3 d-flex flex-row">
                <div class="text-primary fw-bold  h4">Management</div>
                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor"
                    class="bi bi-list ms-5" @click="ShowSb" viewBox="0 0 16 16">
                    <path fill-rule="evenodd"
                        d="M2.5 12a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5m0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5m0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5" />
                </svg>
            </div>
            <div class="col-lg d-flex align-items-center justify-content-end">
                <div class="text-black me-0 align-content-center">
                    Hello, <span class="fw-bold">{{ fullName }}</span>
                </div>
                <div class="dropdown btn">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                        class="bi bi-caret-down-fill" viewBox="0 0 16 16">
                        <path
                            d="M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z" />
                    </svg>
                    <div class="dropdown-content zindex-tooltip">
                        <div class="d-flex flex-row">
                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                                class="bi bi-person-circle my-3" viewBox="0 0 16 16">
                                <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                                <path fill-rule="evenodd"
                                    d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1" />
                            </svg>
                            <div class="pt-0">
                                <NuxtLink :to="`/profile/${accountId}`">Profile</NuxtLink>
                            </div>
                        </div>
                        <div class="d-flex flex-row">
                            <div>
                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor"
                                    class="bi bi-box-arrow-right pt-2 my-2" viewBox="0 0 16 16">
                                    <path fill-rule="evenodd"
                                        d="M10 12.5a.5.5 0 0 1-.5.5h-8a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h8a.5.5 0 0 1 .5.5v2a.5.5 0 0 0 1 0v-2A1.5 1.5 0 0 0 9.5 2h-8A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h8a1.5 1.5 0 0 0 1.5-1.5v-2a.5.5 0 0 0-1 0z" />
                                    <path fill-rule="evenodd"
                                        d="M15.854 8.354a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L14.293 7.5H5.5a.5.5 0 0 0 0 1h8.793l-2.147 2.146a.5.5 0 0 0 .708.708z" />
                                </svg>
                            </div>
                            <div class="p-0">
                                <a href="#" @click="signOut"> Logout </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row border-top">
            <div class="col-lg-2 bg-white p-0" v-if="showSideBar">
                <div class="position-relative sidebar" tabindex="-1">
                    <NuxtLink to="/Dashboard" class="py-3 d-flex flex-row ps-4 text-decoration-none nav-link">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                            class="bi bi-house-fill" viewBox="0 0 16 16">
                            <path
                                d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L8 2.207l6.646 6.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293z" />
                            <path d="m8 3.293 6 6V13.5a1.5 1.5 0 0 1-1.5 1.5h-9A1.5 1.5 0 0 1 2 13.5V9.293z" />
                        </svg>
                        <div class="my-0 ms-2 ">Dashboard</div>
                    </NuxtLink>
                    <div class="sidebar">
                        <ul class="navbar-nav justify-content-end flex-grow-1">
                            <li class="nav-item ps-4">
                                <a class="nav-link" aria-current="page" href="#"></a>
                            </li>
                            <li class="nav-item">
                                <a class="text-primary text-decoration-none fw-bold ps-4" aria-current="page"
                                    href="#">Module</a>
                            </li>
                            <NuxtLink to="/cv" class="nav-link d-flex flex-row active ps-4">
                                <div>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                                        class="bi bi-person-vcard-fill text-danger mt-2" viewBox="0 0 16 16">
                                        <path
                                            d="M0 4a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2zm9 1.5a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 0-1h-4a.5.5 0 0 0-.5.5M9 8a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 0-1h-4A.5.5 0 0 0 9 8m1 2.5a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 0-1h-3a.5.5 0 0 0-.5.5m-1 2C9 10.567 7.21 9 5 9c-2.086 0-3.8 1.398-3.984 3.181A1 1 0 0 0 2 13h6.96q.04-.245.04-.5M7 6a2 2 0 1 0-4 0 2 2 0 0 0 4 0" />
                                    </svg>
                                </div>
                                <div class="my-0 ms-2 mt-2">Quản lý CV</div>
                            </NuxtLink>
                            <NuxtLink to="/Source" class="nav-link d-flex flex-row ps-4">
                                <div>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                                        class="bi bi-tree-fill mt-2 text-success" viewBox="0 0 16 16">
                                        <path
                                            d="M8.416.223a.5.5 0 0 0-.832 0l-3 4.5A.5.5 0 0 0 5 5.5h.098L3.076 8.735A.5.5 0 0 0 3.5 9.5h.191l-1.638 3.276a.5.5 0 0 0 .447.724H7V16h2v-2.5h4.5a.5.5 0 0 0 .447-.724L12.31 9.5h.191a.5.5 0 0 0 .424-.765L10.902 5.5H11a.5.5 0 0 0 .416-.777z" />
                                    </svg>
                                </div>
                                <div class="my-0 ms-2 mt-2">Quản lý nguồn CV</div>
                            </NuxtLink>
                            <NuxtLink to="/Position" class="nav-link d-flex flex-row ps-4">
                                <div>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                                        class="bi bi-bag-dash-fill text-info" viewBox="0 0 16 16">
                                        <path fill-rule="evenodd"
                                            d="M10.5 3.5a2.5 2.5 0 0 0-5 0V4h5zm1 0V4H15v10a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2V4h3.5v-.5a3.5 3.5 0 1 1 7 0M6 9.5a.5.5 0 0 0 0 1h4a.5.5 0 0 0 0-1z" />
                                    </svg>
                                </div>
                                <div class="my-0 ms-2 mt-1">Quản lý vị trí</div>
                            </NuxtLink>

                            <NuxtLink to="/kanban" class="nav-link d-flex flex-row ps-4">
                                <div>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                                        class="bi bi-kanban mt-2 text-warning" viewBox="0 0 16 16">
                                        <path
                                            d="M13.5 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1h-11a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1zm-11-1a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h11a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2z" />
                                        <path
                                            d="M6.5 3a1 1 0 0 1 1-1h1a1 1 0 0 1 1 1v3a1 1 0 0 1-1 1h-1a1 1 0 0 1-1-1zm-4 0a1 1 0 0 1 1-1h1a1 1 0 0 1 1 1v7a1 1 0 0 1-1 1h-1a1 1 0 0 1-1-1zm8 0a1 1 0 0 1 1-1h1a1 1 0 0 1 1 1v10a1 1 0 0 1-1 1h-1a1 1 0 0 1-1-1z" />
                                    </svg>
                                </div>
                                <div class="my-0 ms-2 mt-2">Kanban</div>
                            </NuxtLink>
                            <li v-if="role === 'Admin'" class="quanlitaikhoan position-fixed bottom m-0 w-100">
                                <div class="pt-3 text d-flex flex-row ps-4 col-lg-2">
                                    <div class="sidebar ms-2 text-primary fw-bold">
                                        Admin
                                    </div>
                                </div>
                                <NuxtLink to="/account" class="nav-link d-flex flex-row ps-4 col-lg-2">
                                    <div>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20"
                                            fill="currentColor" class="bi bi-key-fill mt-2" viewBox="0 0 16 16">
                                            <path
                                                d="M3.5 11.5a3.5 3.5 0 1 1 3.163-5H14L15.5 8 14 9.5l-1-1-1 1-1-1-1 1-1-1-1 1H6.663a3.5 3.5 0 0 1-3.163 2M2.5 9a1 1 0 1 0 0-2 1 1 0 0 0 0 2" />
                                        </svg>
                                    </div>
                                    <div class="my-0 ms-2 mt-2">Quản lý tài khoản</div>
                                </NuxtLink>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div :class="(showSideBar ? 'col-lg-10' : 'col-lg-12') +
                ' p-0 bg-body position-relative content-body'
                ">
                <div class="p-2 mb-5">
                    <slot />
                </div>
                <div class="d-flex flex-row bottom-0 footer p-0 border-top m-0 position-fixed">
                    <div :class="(showSideBar ? 'col-lg-4' : 'col-lg-6') + '  mt-2 ps-2  ms-2'
                        ">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                            class="bi bi-c-circle" viewBox="0 0 16 16">
                            <path
                                d="M1 8a7 7 0 1 0 14 0A7 7 0 0 0 1 8m15 0A8 8 0 1 1 0 8a8 8 0 0 1 16 0M8.146 4.992c-1.212 0-1.927.92-1.927 2.502v1.06c0 1.571.703 2.462 1.927 2.462.979 0 1.641-.586 1.729-1.418h1.295v.093c-.1 1.448-1.354 2.467-3.03 2.467-2.091 0-3.269-1.336-3.269-3.603V7.482c0-2.261 1.201-3.638 3.27-3.638 1.681 0 2.935 1.054 3.029 2.572v.088H9.875c-.088-.879-.768-1.512-1.729-1.512" />
                        </svg>
                        2024 <span class="text-primary"> Administrator </span>
                    </div>
                    <div :class="(showSideBar ? 'col-lg-6' : 'col-lg-6') +
                        '  d-flex justify-content-end mt-2 Powered'
                        ">
                        Powered by <span class="text-primary ms-1"> OverSea </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
import Api from '~/service/Base/api.ts';
import { jwtDecode } from 'jwt-decode';

let api = new Api();
let showSideBar = ref(true);

let accountId = ref('');
let userFullName = '';

let role = ref('User');
defineProps({
    FullName: String,
});

definePageMeta({
    middleware: 'auth',
});
const ShowSb = () => {
    showSideBar.value = !showSideBar.value;
};

if (typeof localStorage !== 'undefined') {
    const jwtToken = localStorage.getItem('token');
    if (jwtToken) {
        try {
            const decodedToken = jwtDecode(jwtToken);
            role = decodedToken.Role;
            userFullName = decodedToken.UserFullName;
            accountId = decodedToken.Id;
        } catch (error) {
            console.error('Error decoding JWT:', error);
        }
    }
}

let fullName = ref(userFullName);

const signOut = () => {
    api.postAPI('/Account/logout', {}).then((res) => {
        localStorage.removeItem('token');
        localStorage.clear();
        navigateTo('/');
    });
};
</script>
