<template>
    <CVModal id_model="deletePositionModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Xóa vị trí này</slot>
        </template>
        <template #body>
            <p>Bạn có chắc chắn muốn xóa vị trí này?</p>
            <div class="modal-footer align-content-center justify-content-center">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                    Hủy bỏ
                </button>
                <button type="button" class="btn btn-danger" @click="deletePosition">
                    Xóa
                </button>
            </div>
        </template>
    </CVModal>
</template>
<script setup>
import axios from 'axios';
import Api from '~/service/Base/api.ts';
import { defineProps, defineEmits } from 'vue';

const props = defineProps({
    positionId: {
        type: String,
        required: true,
    },
});

const api = new Api();
const emits = defineEmits(['position-deleted', 'hide-modal']);
const deletePosition = () => {
    const id = props.positionId;
    api.deleteById('/Position', id)
        .then((res) => {
            emits('position-deleted', id);
            emits('hide-modal');
            $('.btn-close').click();
        })
        .catch((error) => {
            console.error('Lỗi khi xóa vị trí:', error);
        });
};
</script>

<style scoped></style>
