<template>
    <section class="card my-5">
        <article class="card-header">
            <h5>User Profile</h5>
        </article>
        <article class="card-body">
            <form @submit.prevent="updateProfile(accountId, account)">
                <div class="row mb-3 form-group required">
                    <label for="email" class="col-sm-2 col-form-label control-label text-end">Email</label>
                    <div class="col-sm-9">
                        <input type="email" v-model="account.email" class="form-control" required>
                    </div>
                </div>

                <div class="row mb-3 form-group required">
                    <label for="fullName" class="col-sm-2 col-form-label control-label text-end">
                        Họ tên
                    </label>
                    <div class="col-sm-9">
                        <input type="text" v-model="account.fullName" class="form-control">
                    </div>
                </div>
                <div class="row mb-3 form-group required">
                    <label for="phone" class="col-sm-2 col-form-label control-label text-end">Điện thoại</label>
                    <div class="col-sm-9">
                        <input type="text" v-model="account.phone" class="form-control">
                    </div>
                </div>

                <div class="row mb-3 form-group">
                    <label for="authority" class="col-sm-2 col-form-label control-label text-end">Quyền</label>
                    <div class="col-sm-9">
                        <select class="form-select" v-model="account.roleId" disabled aria-label="Default select example">
                            <option v-for="role in roles" :value="role.id">
                                {{ role.roleName }}
                            </option>
                        </select>
                    </div>
                </div>
                <div class="align-items-center justify-content-center text-center mt-5">
                    <button type="submit"
                            class="btn btn-primary mx-2">
                        <i class="fa-solid fa-floppy-disk me-1"></i>
                        Cập nhật
                    </button>
                    <NuxtLink class="text-decoration-none" data-bs-toggle="modal" data-bs-target="#changePasswordModal">
                        <button type="button" class="btn btn-outline-primary me-2">
                            <i class="fa-solid fa-retweet"></i>
                            Đổi mật khẩu
                        </button>
                    </NuxtLink>
                    <NuxtLink to="/Dashboard" class="text-decoration-none">
                        <button type="button" class="btn btn-outline-secondary me-2">
                            <i class="fa-solid fa-left-long mx-1"></i>
                            Quay lại
                        </button>
                    </NuxtLink>
                </div>
            </form>
        </article>
        <ChangePasswordModal :accountId="accountId" />
    </section>

</template>

<script>
import Api from '~/service/Base/api.ts';

const api = new Api();
definePageMeta({
    middleware: 'auth'
})

export default {
    name: "[id]",
    data() {
        return {
            accountId: '',
            roles: [],
            account: {
                id: '',
                email: '',
                password: '',
                fullName: '',
                phone: '',
                roleId: 2,
            },
        };
    },
    mounted() {
        this.accountId = this.$route.params.id;
        this.getAccount(this.accountId);
        this.getRoles();
    },
    methods: {
        getAccount(id) {
            api.get(`/Account/findbyid/${id}`, null).then(res => {
                this.account = {...res.data.responseData}
            })
        },
        updateProfile(id, account) {
            api.putAPI(`/Account/${id}`, account).then(res => {
                    this.getAccount(id);
            }).catch(err => {
                console.log(err);
            });
        },
        getRoles() {
            api.get(`/Role`, null)
                .then((res) => {
                    this.roles = res.data.responseData;
                })
                .catch((err) => console.log(err));
        },
    },
};

</script>

<style scoped>

</style>