<template>
    <CVModal id_model="deleteSourceModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Xóa nguồn CV này</slot>
        </template>
        <template #body>
            <p>Bạn có chắc chắn muốn xóa nguồn CV này?</p>
            <div class="modal-footer align-content-center justify-content-center">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                    Hủy bỏ
                </button>
                <button type="button" class="btn btn-danger" @click="deleteSource">
                    Xóa
                </button>
            </div>
        </template>
    </CVModal>
</template>

<script setup>
import axios from 'axios';
import { defineProps, defineEmits } from 'vue';
import Api from '~/service/Base/api.ts';
const props = defineProps({
    sourceId: {
        type: String,
        required: true,
    },
});
const api = new Api();
const emits = defineEmits(['source-deleted', 'hide-modal']);

const deleteSource = () => {
    const id = props.sourceId;
    api.deleteById('/Source', id)
        .then((res) => {
            emits('source-deleted', id);
            emits('hide-modal');
            $('.btn-close').click();
        })
        .catch((error) => {
            console.error('Lỗi khi xóa nguồn:', error);
        });
};
</script>

<style scoped></style>
