<template>
    <CVModal id_model="deleteCVModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title> Cảnh báo </template>
        <template #body>
            <p>Bạn có chắc chắn muốn xóa CV này?</p>
            <div class="text-end">
                <button type="button" class="btn btn-secondary me-2" data-bs-dismiss="modal">
                    Hủy bỏ
                </button>
                <button type="button" class="btn btn-danger" @click="deleteCV">
                    Xóa
                </button>
            </div>
        </template>
    </CVModal>
</template>
<script setup>
import { defineProps, defineEmits } from 'vue';
import API from '../../service/Base/api.ts';
import { useCvStore } from '~/stores/cvStore.js';
const cvStore = useCvStore();
const api = new API();
const emit = defineEmits(['reloadCV']);
const props = defineProps({
    data_bs_target_modal: {
        type: String,
        default: '',
    },
    id_model: {
        type: String,
        default: '',
    },
    idCv: {
        type: Number,
        required: true,
    },
});

/**
 * Hanlde delete CV by id
 */
const deleteCV = async () => {
    var response = await api.deleteById('/CV', props.idCv);
    if (Math.floor(response.data.statusCode / 100) == 2) {
        emit('reloadCV');
        $('.btn-close').click();
    }
    if (findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', props.idCv) != -1) {
        let indexMail = findIndexByAttribute(cvStore.emailsEditing, 'IdCvNeedToSendMail', props.idCv)
        cvStore.emailsEditing.splice(indexMail, 1)
    }

    if (findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', props.idCv) != -1) {
        let index = findIndexByAttribute(cvStore.selectedCvsToSendMail, 'id', props.idCv)
        cvStore.selectedCvsToSendMail.splice(index, 1)
    }
};
</script>
