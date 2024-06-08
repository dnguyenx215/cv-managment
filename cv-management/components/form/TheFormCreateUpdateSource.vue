<template>
    <Form
        @submit.prevent="handleSubmit"
        :validation-schema="_validationSchema"
        v-slot="{ errors }"
        ref="formRef"
    >
        <CVModal id_model="create-update-source-modal">
            <template #icon>
                <slot name="icon"></slot>
            </template>
            <template #title>
                <span v-if="!isEditMode">
                    <slot name="header">Thêm nguồn CV</slot>
                </span>
                <span v-else>
                    <slot name="header">Cập nhật nguồn CV</slot>
                </span>
            </template>
            <template #body v-slot="slotProps">
                <div class="row mb-3 form-group required">
                    <label
                        for="source-name"
                        class="col-sm-3 col-form-label control-label text-end"
                    >
                        Tên Nguồn CV
                    </label>
                    <div class="col-sm-9">
                        <Field
                            name="nameSource"
                            v-model="source.nameSource"
                            type="text"
                            class="form-control"
                        />

                        <div v-show="errors.nameSource" class="error-message text-danger">
                            {{ errors.nameSource }}
                        </div>
                    </div>
                </div>
                <div class="modal-footer align-content-center justify-content-center">
                    <button
                        v-if="isEditMode"
                        @click="updatesource"
                        type="submit"
                        class="btn btn-sm btn-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="0.88em"
                            height="1em"
                            viewBox="0 -70 700 700"
                        >
                            <path
                                fill="#f7f7f7"
                                d="m433.941 129.941l-83.882-83.882A48 48 0 0 0 316.118 32H48C21.49 32 0 53.49 0 80v352c0 26.51 21.49 48 48 48h352c26.51 0 48-21.49 48-48V163.882a48 48 0 0 0-14.059-33.941M224 416c-35.346 0-64-28.654-64-64c0-35.346 28.654-64 64-64s64 28.654 64 64c0 35.346-28.654 64-64 64m96-304.52V212c0 6.627-5.373 12-12 12H76c-6.627 0-12-5.373-12-12V108c0-6.627 5.373-12 12-12h228.52c3.183 0 6.235 1.264 8.485 3.515l3.48 3.48A11.996 11.996 0 0 1 320 111.48"
                            />
                        </svg>
                        Lưu
                    </button>
                    <button
                        v-else
                        type="submit"
                        @click="createsource"
                        class="btn btn-sm btn-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="10px"
                            height="10px"
                            viewBox="0 -70 700 700"
                        >
                            <path
                                fill="#f7f7f7"
                                d="m433.941 129.941l-83.882-83.882A48 48 0 0 0 316.118 32H48C21.49 32 0 53.49 0 80v352c0 26.51 21.49 48 48 48h352c26.51 0 48-21.49 48-48V163.882a48 48 0 0 0-14.059-33.941M224 416c-35.346 0-64-28.654-64-64c0-35.346 28.654-64 64-64s64 28.654 64 64c0 35.346-28.654 64-64 64m96-304.52V212c0 6.627-5.373 12-12 12H76c-6.627 0-12-5.373-12-12V108c0-6.627 5.373-12 12-12h228.52c3.183 0 6.235 1.264 8.485 3.515l3.48 3.48A11.996 11.996 0 0 1 320 111.48"
                            />
                        </svg>
                        Thêm
                    </button>
                    <button
                        type="reset"
                        class="btn btn-sm btn-outline-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 -70 700 700"
                            class="icon"
                            width="10px"
                            height="10px"
                        >
                            <path
                                fill="#006eff"
                                d="m257.5 445.1l-22.2 22.2c-9.4 9.4-24.6 9.4-33.9 0L7 273c-9.4-9.4-9.4-24.6 0-33.9L201.4 44.7c9.4-9.4 24.6-9.4 33.9 0l22.2 22.2c9.5 9.5 9.3 25-.4 34.3L136.6 216H424c13.3 0 24 10.7 24 24v32c0 13.3-10.7 24-24 24H136.6l120.5 114.8c9.8 9.3 10 24.8.4 34.3"
                            />
                        </svg>
                        Huỷ bỏ
                    </button>
                </div>
            </template>
        </CVModal>
    </Form>
</template>

<script setup>
import { ref, watch, defineProps, defineEmits } from 'vue';
import Api from '~/service/Base/api.ts';
import { Form, Field } from 'vee-validate';

const api = new Api();
const formRef = ref(null);
const emits = defineEmits(['source-saved']);

/**
 * received data
 */
const props = defineProps({
    editSource: {
        type: Object,
        default: null,
        required: false,
    },
    isEditMode: {
        type: Boolean,
        default: false,
        required: true,
    },
});

/**
 * reset form
 */
const resetForm = () => {
    if (formRef.value) {
        formRef.value.resetForm();
    }
};

/**
 * define source
 */
const source = ref({
    nameSource: '',
});

/**
 * validate
 */
const _validationSchema = {
    nameSource: 'required',
};

/**
 * insert source
 */
const createsource = () => {
    if (!source.value.nameSource) {
        return;
    }
    const formData = new FormData();
    formData.append('nameSource', source.value.nameSource);

    api.postAPI('/source', formData)
        .then((res) => {
            emits('source-saved');
            $('#create-update-source-modal').modal('hide');
        })
        .catch((error) => {
            console.error('Error creating source:', error);
        });
    resetForm();
};

/**
 * update source
 */
const updatesource = () => {
    if (!source.value.nameSource) {
        return;
    }
    const data = {
        nameSource: source.value.nameSource,
        id: props.editSource.id,
    };

    api.putAPI(`/Source/${props.editSource.id}`, data)
        .then((res) => {
            emits('source-saved');
            $('#create-update-source-modal').modal('hide');
        })
        .catch((error) => {
            console.error('Error updating source:', error);
        });
    resetForm();
};

/**
 * handle submit
 */
const handleSubmit = () => {
    if (props.isEditMode) {
        updatesource();
    } else {
        createsource();
    }
};

/**
 * update edit source
 */
watch(
    () => props.editSource,
    (newVal) => {
        if (newVal.nameSource) {
            source.value = { ...newVal };
        }
    },
    { immediate: true },
);
</script>
