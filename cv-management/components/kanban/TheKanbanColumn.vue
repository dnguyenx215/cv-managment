<script setup>
import { getRandomBackgroundColor, useDebounce } from '~/assets/js/helper.js';
import { apiFactory } from '~/plugins/api';
import CV_CONSTANT from 'assets/js/constants/constants.js';
import { useCvStore } from '~/stores/cvStore';
import { computed, ref, watch, inject, nextTick } from 'vue';

const props = defineProps({
    // itemsDraggable: {
    //     type: Array,
    //     default: [],
    // },
    log: {
        type: Function,
        default: () => { },
    },
    bgColor: {
        type: String,
        default: "bg-success",
    },
    group: {
        type: Object
    }
});
const emit = defineEmits(['columnDeleted']);


//drag column in the future
const itemsDraggable = defineModel('itemsDraggable')
const title = ref(itemsDraggable.value.columnName)


const updateColumnTitle = async (event) => {
    const res = await apiFactory.columnLayout.updateColumnLayout({ columnName: event.target.value, id: itemsDraggable.value.id })
}

const debounceUpdateColumnTitle = useDebounce(() => {
    updateColumnTitle()
}, 500);

const deleteColumn = async (column) => {
    await apiFactory.columnLayout.deleteColumnLayout({ id: column.id });
    emit('columnDeleted', column.columnName);

}

const isDisabledEditColumnTitle = (column) => {
    return column.columnNam?.toLowerCase() === CV_CONSTANT.UnknownStr.toLowerCase();

}

const isHiddenDeleteColumnButton = (column) => {
    switch (column.columnName) {
        case CV_CONSTANT.DefaultColumnTitle.REVIEW_CV_WAITING:
            return true;
        case CV_CONSTANT.DefaultColumnTitle.REVIEW_CV_FAIL:
            return true;
        case CV_CONSTANT.DefaultColumnTitle.REVIEW_CV_PASS:
            return true;
        case CV_CONSTANT.DefaultColumnTitle.INTERVIEW_RES_FAIL:
            return true;
        case CV_CONSTANT.DefaultColumnTitle.INTERVIEW_RES_PASS:
            return true;
        case CV_CONSTANT.DefaultColumnTitle.BACKUP:
            return true;
        case CV_CONSTANT.DefaultColumnTitle.UNKNOWN:
            return true;
        default:
            return false;
    }
}

watch([title], ([newTitle], [oldTitle]) => {
    debounceUpdateColumnTitle();
});
</script>
<template>
    <div class="col kanban-col flex-nowrap">
        <div class=" rounded border ">
            <div class="border-secondary border-bottom table-wip-caption  pt-2 pb-2 sticky-top d-flex align-items-center justify-content-between px-1"
                :style="{ 'background-color': getRandomBackgroundColor() }">
                <h6 class="mb-0 fw-medium">
                    <slot name="name-column"></slot>
                    <input class="bg-transparent border-0 rounded px-1 w-100 focus-bg-white "
                        :disabled="isDisabledEditColumnTitle(itemsDraggable)" @keyup.enter="($event.target).blur()"
                        type="text" v-model="title" @change="updateColumnTitle" />
                </h6>
                <span class="badge bg-secondary me-1">{{ itemsDraggable.cVs.length }}</span>
                <span class="badge bg-danger" style="cursor: pointer;"
                    :hidden="isHiddenDeleteColumnButton(itemsDraggable)" @click="deleteColumn(itemsDraggable)">
                    <i class="fa-solid fa-xmark" style="font-size: 12px;"></i>
                </span>
            </div>
            <div class="table-wip overflow-scroll bg-body-tertiary px-1 py-2">
                <draggable :list="itemsDraggable.cVs" :group="group" ghostClass="ghost-cls" chosenClass="chosen-cls"
                    dragClass="drag-cls" class="dragArea" @change="log">

                    <the-kanban-item class="ticket mb-1" v-for="element in itemsDraggable.cVs"
                        :name="element.id.toString()" :modalItem="element" :isSendMail="itemsDraggable.isSendMail">
                    </the-kanban-item>
                </draggable>
                <!-- <draggable :list="itemsDraggable.cVs" ghost-class="ghost" :force-fallback="true"
                    chosen-class="chosenClass" animation="300" group="itxst" :fallback-class="true"
                    :fallback-on-body="true" :sort="false">
                    <template #item="{ element }">
                        <div class="item move">
                            <label class="move">{{ element.isSendMail }}</label>
                        </div>
                    </template>
</draggable> -->
            </div>
        </div>
    </div>
</template>

<style scoped>
/**/
.ghost-cls {
    opacity: 0.3;
    background: rgb(248, 187, 187);
    border-radius: 4px;
    padding: 1rem;
    margin: 0.5rem auto;
    box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.35);
}

/**/

.chosen-cls {
    opacity: 0.6;
    color: rgb(75, 73, 73);
    background: rgb(104, 147, 232);
    box-shadow: 2px 2px 8px rgba(50, 50, 50, 0.35);
}

.drag-cls {
    opacity: 0.7;
    background: rgb(190, 252, 190);
    /** /
  background: rgb(252, 195, 91);
  /**/
    color: rgb(20, 20, 20);
    border-radius: 12px;
    padding: 1rem;
    margin: 0.5rem auto;
    box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.35);
}



.kanban-col {
    /* min-height: var(--height-dragArea) !important; */
    min-width: 20rem;
}

.table-wip {
    grid-area: wip;
    /* min-height: var(--height-dragArea); */
    max-height: 550px;
    overflow: scroll;
    /** /
  background: rgb(140, 210, 255);
  /**/
}

.dragArea {
    /**/
    min-height: 4px;
    /**/
    /**/
    /* min-height: var(--height-dragArea); */
    height: 100%;
    /**/

    /**/
}

/* width */
::-webkit-scrollbar {
    width: 1px;
    height: 2px;
}

/* Track */
::-webkit-scrollbar-track {
    background: #fff;
    border-radius: 5px;
}

/* Handle */
::-webkit-scrollbar-thumb {
    background: #cdd3d6;
    border-radius: 5px;
}

::-webkit-scrollbar-thumb:hover {
    background: #555;
    cursor: pointer;
}
</style>
