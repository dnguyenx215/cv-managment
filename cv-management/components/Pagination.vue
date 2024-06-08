<template>
    <div>
        <nav aria-label="Page navigation example">
            <div class="pagination">
                <button @click="props.setPage(1)" :class="{ 'disabled-button': props.currentPage === 1 }"
                    class="page-link btn-arrow" aria-label="Previous">
                    <div class="btn-svg">
                        <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M11.7265 12L12.6665 11.06L9.61317 8L12.6665 4.94L11.7265 4L7.7265 8L11.7265 12Z"
                                fill="#333333" />
                            <path d="M7.33344 12L8.27344 11.06L5.2201 8L8.27344 4.94L7.33344 4L3.33344 8L7.33344 12Z"
                                fill="#333333" />
                        </svg>
                    </div>
                </button>
                <button @click="props.previousPage" :class="{ 'disabled-button': props.currentPage === 1 }"
                    class="page-link btn-arrow" aria-label="Previous">
                    <div class="btn-svg">
                        <svg width="5" height="8" viewBox="0 0 5 8" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M4.06 8L5 7.06L1.94667 4L5 0.94L4.06 8.21774e-08L0.0599996 4L4.06 8Z"
                                fill="black" />
                        </svg>
                    </div>
                </button>
                <span style="cursor: pointer;" :class="{ 'activePagination': props.currentPage == 1 }"
                    @click="props.setPage(1)" class="page-item page-link">1
                </span>
                <span v-for="page in displayPages" :key="page">
                    <span v-if="page === '...'">{{ page }}</span>
                    <span v-else style="cursor: pointer;" :class="{ 'activePagination': page === props.currentPage }"
                        @click="props.setPage(page)" class="page-item page-link">
                        {{ page }}
                    </span>
                </span>
                <span v-if="props.totalPages > 1" style="cursor: pointer;" :class="{ 'activePagination': props.currentPage === props.totalPages }"
                    @click="props.setPage(props.totalPages)" class="page-item page-link">{{ props.totalPages }}
                </span>
                <button @click="props.nextPage" :class="{ 'disabled-button': props.currentPage === props.totalPages }"
                    class="page-link btn-arrow" aria-label="Next">
                    <div class="btn-svg">
                        <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M6.94 4L6 4.94L9.05333 8L6 11.06L6.94 12L10.94 8L6.94 4Z" fill="black" />
                        </svg>
                    </div>
                </button>
                <button @click="props.setPage(props.totalPages)" :class="{ 'disabled-button': props.currentPage === props.totalPages }"
                    class="page-link btn-arrow" aria-label="Next">
                    <div class="btn-svg">
                        <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M4.2735 4L3.3335 4.94L6.38683 8L3.3335 11.06L4.2735 12L8.2735 8L4.2735 4Z"
                                fill="black" />
                            <path d="M8.66656 4L7.72656 4.94L10.7799 8L7.72656 11.06L8.66656 12L12.6666 8L8.66656 4Z"
                                fill="black" />
                        </svg>
                    </div>
                </button>
            </div>
        </nav>
    </div>
</template>

<script setup>
import { defineProps } from 'vue';
const props = defineProps({
    currentPage: {
        type: Number,
        required: true
    },
    totalPages: {
        type: Number,
        required: true
    },
    nextPage: {
        type: Function,
        required: true
    },
    previousPage: {
        type: Function,
        required: true
    },
    setPage: {
        type: Function,
        required: true
    }
});
const visiblePages = 1;

/**
 * Handle pagination
 */
const displayPages = computed(() => {
    const start = Math.max(2, props.currentPage - visiblePages);
    const end = Math.min(props.totalPages - 1, props.currentPage + visiblePages);
    const pages = [];
    for (let i = start; i <= end; i++) {
        pages.push(i);
    }
    if (start > 2) {
        pages.unshift('...');
    }
    if (end < props.totalPages - 1) {
        pages.push('...');
    }
    return pages;
});

</script>
<style>
.activePagination {
    background-color: #2F80ED;
    color: #fff !important;
    border-color: #2F80ED;
}

.page-link {
    color: black;
    border-radius: 50%;
    margin: 1px;
    line-height: 20px;
    &:hover {
        background-color: #2F80ED;
        color: #fff !important;
        border-color: #2F80ED;
    }
}

.disabled-button {
    color: gray !important;
    cursor: not-allowed;
    pointer-events: none;
}

.btn-arrow {
    padding: 6px;
    display: flex;
    align-items: center;
}

.btn-svg {
    width: 20px;
    height: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>