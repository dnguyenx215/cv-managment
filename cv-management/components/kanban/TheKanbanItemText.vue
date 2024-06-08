<script setup>
import { defineProps } from 'vue';
const props = defineProps({
    customStyle: {
        type: String,
    }
});
</script>
<template>
    <div class="p-1 ps-3 row g-1" :class="customStyle">
        <span class="col-1">

            <slot name="icon"></slot>
        </span>
        <span class="col-11">
            <slot name="information-cv"></slot>
        </span>
    </div>
</template>
