<template>
    <CVModal id_model="deleteAccountModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Xóa tài khoản</slot>
        </template>
        <template #body>
            <p>Bạn có chắc chắn muốn xóa tài khoản này?</p>
            <div class="modal-footer align-content-center justify-content-center">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                    Hủy bỏ
                </button>
                <button type="button" class="btn btn-danger" @click="deleteAccount">
                    <i class="fa-regular fa-trash-can mx-1"></i>
                    Xóa
                </button>
            </div>
        </template>
    </CVModal>
</template>

<script>
import Api from '~/service/Base/api.ts';


const api =  new Api();

export default {
    props: {
        accountId: {
            type: String,
            required: true,
        },
    },
    data() {
        return {};
    },
    methods: {
        deleteAccount() {
            const id = this.accountId;
            api.deleteById(`/Account`, id).then((res) => {
                this.$emit('account-deleted', id);
                $('#deleteAccountModal').modal('hide');
                $('.btn-close').click();
            });
        },
    },
};
</script>

<style scoped>
/* Tùy chỉnh kiểu dáng của modal */
</style>
