<template>
    <div>
        <div :class="customClass" class="item p-4 rounded-3 mb-4 mx-1">
            <div class="quantity text-white h4">
                <slot name="quantity"></slot>
            </div>
            <div class="status text-uppercase text-white">
                <slot name="name"></slot>
            </div>
            <div class="icon text-end">
                <slot name="icon"></slot>
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineProps } from 'vue';
const props = defineProps({
    customClass: {
        type: String,
        required: true,
    },
});
</script>
