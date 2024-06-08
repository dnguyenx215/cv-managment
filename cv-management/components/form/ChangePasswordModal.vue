<template>
    <CVModal id_model="changePasswordModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Đổi mật khẩu</slot>
        </template>
        <template #body>
            <form @submit.prevent="handleSubmit()">
                <div v-if="!isAdminChangeMode" class="row mb-3 form-group required">
                    <label for="old-pass" class="col-sm-3 col-form-label control-label text-end">
                        Mật khẩu hiện tại
                    </label>
                    <div class="col-sm-9">
                        <input type="password" v-model="currentPassword" class="form-control">
                        <span :hidden="isHidden('currentPassword')" class="error-message text-danger">
                            Mật khẩu hiện tại không được để trống.
                        </span>
                    </div>
                </div>
                <div class="row mb-3 form-group required">
                    <label for="new-pass" class="col-sm-3 col-form-label control-label text-end">Mật khẩu mới</label>
                    <div class="col-sm-9">
                        <input type="password" v-model="newPassword" class="form-control">
                        <span :hidden="isHidden('newPassword')" class="error-message text-danger">
                            * Chú ý: <br>
                            <i>
                                - Mật khẩu phải có ít nhất 5 ký tự. <br>
                                - Chứa ít nhất một chữ hoa, một chữ thường, một số.
                            </i>
                        </span>

                    </div>
                </div>
                <div class="row mb-3 form-group required">
                    <label for="cf-pass" class="col-sm-3 col-form-label control-label text-end">
                        Xác nhận mật khẩu
                    </label>
                    <div class="col-sm-9">
                        <input type="password" v-model="confirmPassword" class="form-control">
                        <span :hidden="isHidden('confirmPassword')" class="error-message text-danger">
                            Xác nhận mật khẩu không khớp với mật khẩu mới.
                        </span>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <button type="submit" class="btn btn-secondary  d-flex align-items-center" data-bs-dismiss="modal">
                        Huỷ bỏ
                    </button>
                    <button type="submit" class="btn btn-warning d-flex align-items-center">
                        Thay đổi
                    </button>
                </div>
            </form>
        </template>
    </CVModal>
</template>

<script>
    import { defineComponent } from 'vue';
    import Api from '~/service/Base/api';

    const api = new Api();

    export default defineComponent({
        name: 'ChangePasswordModal',
        props: {
            accountId: {
                type: String,
                required: true
            },
            isAdminChangeMode: {
                type: Boolean,
                required: false,
                default: false
            }
        },
        data() {
            return {
                currentPassword: '',
                newPassword: '',
                confirmPassword: '',

            };
        },
        methods: {
            isHidden(field) {
                // Kiểm tra trường mật khẩu hiện tại
                if (field === 'currentPassword') {
                    return this.validateOldPassword();
                }
                // Kiểm tra trường mật khẩu mới
                else if (field === 'newPassword') {
                    return this.validatePassword();
                }
                // Kiểm tra trường xác nhận mật khẩu
                else if (field === 'confirmPassword') {
                    return this.validateConfirmPassword();
                }
                if (field === 'all') {
                    return this.validateOldPassword() && this.validatePassword() && this.validateConfirmPassword();
                }
                return true; // Mặc định ẩn tất cả thông báo lỗi nếu không có trường nào được chỉ định
            },
            validatePassword() {
                if (this.newPassword.length < 5) {
                    return false;
                }

                const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,}$/;
                return passwordRegex.test(this.newPassword);
            },
            validateConfirmPassword() {
                return this.newPassword === this.confirmPassword;
            },
            validateOldPassword() {
                return this.currentPassword.trim().length > 0;
            },
            handleSubmit() {
                if (!this.isAdminChangeMode) {
                    if (this.validateOldPassword() && this.validatePassword() && this.validateConfirmPassword()) {
                        api.putAPI(`/Account/changePassword?accountId=${this.accountId}&oldPassword=${this.currentPassword}&newPassword=${this.newPassword}`, {}).then(response => {
                            $('#changePasswordModal').modal('hide');
                            $('.btn-close').click();
                            this.currentPassword = '';
                            this.newPassword = '';
                            this.confirmPassword = '';
                        }).catch(err => {
                            console.log(err);
                        })
                    }
                }
                else {
                    if (this.validatePassword() && this.validateConfirmPassword()) {
                        api.putAPI(`/Account/editPassword/${this.accountId}?password=${this.newPassword}`, {}).then(response => {
                            localStorage.removeItem('token')
                            $('#changePasswordModal').modal('hide');
                            $('.btn-close').click();
                            this.newPassword = '';
                            this.confirmPassword = '';
                        }).catch(err => {
                            console.log(err);
                        })
                    }
                }

            },
        },
    });
</script>

<style scoped></style>
