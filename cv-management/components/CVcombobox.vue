<!-- <script setup>
import { computed, ref, watch, inject, nextTick } from 'vue';
import MISAInputWithIconVue from '@/components/base/input/MISAInputWithIcon.vue';
import { findIndexByAttribute } from '@/assets/js/helper.js';
import BEnum from '~/assets/js/enum/BEnum';

const props = defineProps({
    label: String,
    value: String,
    placeholder: String,
    disabled: {
        type: Boolean,
    },
    modelValue: String,
    dataList: Array,
    typeField: String,
    isInputHavingIcon: Boolean,
    isReadonly: Boolean,
    tabIndex: String,
    errorText: String,
});
console.log(props.dataList);
const emit = defineEmits(['update:modelValue', 'setId', 'reloadData']);

const show = ref(false);
//Kiểm soát ô input, xly việc lưu giữ giá trị nhập vào ô input
const search = ref('');

//Phần tử được lựa chọn khi click hoặc enter
const selected = ref('');

//Trạng thái 1. Đang ở father, không nhập input 2. Khi nhập input, chuyển sang dạng con
const state = ref(BEnum.COMBO_BOX.all_selection);

//Quản lý dropdown cha và dropdown con
const hoveredIndex = ref(-1);
const hoveredIndexSub = ref(-1);
const selectDropdownIndex = ref(-1);
const selectDropdownIndexSub = ref(-1);
const inputRef = ref(null);
const inputRefSub = ref(null);

//Quản lý việc click vào ra combobox
const inputRefCombobox = ref(null);

//Lưu trữ model value cho việc binding 2 chiều
const valueLocal = computed({
    get() {
        return props.modelValue;
    },
    set(val) {
        emit('update:modelValue', val);
        // emit('setName', val);
    },
});

/**
 * Copy giá trị từ store vào local component
 */
let properties = computed(() => {
    return props.dataList;
});

//Quản lý hiển thị thông báo lỗi
let isDisplayErrorMessage = ref(false);

/**
 * Chức năng: Đóng combobox data khi mất focus
 * @param {event} - lưu giá trị tham chiếu đến toàn bộ combobox
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
const clickOutSideCombobox = async (event) => {
    try {
        isDisplayErrorMessage.value = true;
        await nextTick();
        if (inputRefCombobox.value) {
            if (!inputRefCombobox.value.contains(event.target)) {
                hiddenComboboxData();
            }
        }
    } catch (error) {
        console.log(
            '🚀 ~ file: MISACombobox.vue:78 ~ clickOutSideCombobox ~ error:',
            error,
        );
    }

    // state.value == BEnum.COMBO_BOX.all_selection;
};

/**
 * Chức năng: Đóng combobox data khi mất focus
 * @param
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
const hiddenComboboxData = () => {
    try {
        inputRef.value.blur();

        show.value = false;

        window.removeEventListener('click', clickOutSideCombobox);

        isRotating.value = false;
        $emit('blur-input'); //dùng để validate khi blur ra khỏi input
    } catch (error) {
        console.log(
            '🚀 ~ file: MISACombobox.vue:190 ~ hiddenComboboxData ~ error:',
            error,
        );
    }
};
/**
 * Chức năng: Di chuyển các lựa chọn trong combo box main, tăng hoverIndex lên
 * @param {event} -
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
const onKeyPressAll = async (e) => {
    if (e.key === 'ArrowDown') {
        console.log('downarrow');
        e.preventDefault();
        inputRef.value.focus();
        hoveredIndex.value++;
        console.log(properties.value.length);
        if (hoveredIndex.value > properties.value.length - 1) {
            hoveredIndex.value = 0;
            console.log('dong 116: lỗi properties.value.length');
        }
        console.log(hoveredIndex.value);
    } else if (e.key === 'ArrowUp') {
        e.preventDefault();
        hoveredIndex.value--;
        if (hoveredIndex.value < 0) {
            hoveredIndex.value = properties.value.length - 1;
        }
    } else if (e.key === 'Enter') {
        selectDropdownIndex.value = hoveredIndex.value;
        selected.value = properties.value[hoveredIndex.value];
        console.log('dong 77 comboboxsaasdasdsa');
        console.log(selected.value);
        isRotating.value = false;
        inputRef.value.focus();
        show.value = false;
        await nextTick();
        emit('setId', selected.value[`${props.typeField}Id`]);

        if (props.typeField == 'Department' || props.typeField == 'TypeProperty') {
            emit('setName', {
                type: `${props.typeField}`,
                value: selected.value[`${props.typeField}Name`],
            });
            handleChangeModelValue(selected.value[`${props.typeField}Code`]);
        } else {
            handleChangeModelValue(selected.value[`value`]);
        }
    }
};

/**
 * Chức năng: Di chuyển các lựa chọn trong combo box sub
 * @param {event} -
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
const onKeyPressSub = async (e) => {
    console.log('sub');
    if (e.key === 'ArrowDown') {
        e.preventDefault();
        inputRefSub.value.focus();
        hoveredIndexSub.value++;
        if (hoveredIndexSub.value > subdFilteredProperties.value.length - 1) {
            hoveredIndexSub.value = 0;
        }
    } else if (e.key === 'ArrowUp') {
        e.preventDefault();
        inputRefSub.value.focus();
        hoveredIndexSub.value--;
        if (hoveredIndexSub.value < 0) {
            hoveredIndexSub.value = subdFilteredProperties.value.length - 1;
        }
    } else if (e.key === 'Enter') {
        selectDropdownIndexSub.value = hoveredIndexSub.value;
        selected.value = subdFilteredProperties.value[hoveredIndexSub.value];
        console.log(selected.value);
        var indexMappingFromSub = findIndexByAttribute(
            props.dataList,
            '',
            selected.value,
        );
        selectDropdownIndex.value = indexMappingFromSub;
        show.value = false;
        isRotating.value = false;
        //Chuyển mode về father selection
        state.value = BEnum.COMBO_BOX.all_selection;

        inputRef.value.focus();
        await nextTick();
        emit('setId', selected.value[`${props.typeField}Id`]);
        if (props.typeField == 'Department' || props.typeField == 'TypeProperty') {
            emit('setName', {
                type: `${props.typeField}`,
                value: selected.value[`${props.typeField}Name`],
            });
            handleChangeModelValue(selected.value[`${props.typeField}Code`]);
        } else {
            handleChangeModelValue(selected.value[`value`]);
        }
    }
};

/**
 * Chức năng: Lọc giá trị dựa theo value input cha
 * @param {event} -
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
let subdFilteredProperties = computed(() => {
    var subdFilteredPropertiesArray = properties.value.filter((p) =>
        p[`${props.typeField}Name`].includes(search.value),
    );

    // mapping từ selection của cha sang selection của con
    console.log(selectDropdownIndex.value);
    console.log(selected.value);
    if (
        findIndexByAttribute(
            subdFilteredPropertiesArray,
            `${props.typeField}Name`,
            selected.value[`${props.typeField}Name`],
        ) >= 0
    ) {
    }
    var indexSubSelect = findIndexByAttribute(
        subdFilteredPropertiesArray,
        `${props.typeField}Name`,
        selected.value[`${props.typeField}Name`],
    );
    selectDropdownIndexSub.value = indexSubSelect;
    if (search.value == '') {
        console.log('ko search gi ca');
        handleChangeModelValue('');
        emit('setId', '');
        emit('reloadData');
    }
    return subdFilteredPropertiesArray;
});

/**
 * Chức năng: Mở đóng phần lựa chọn của combobox
 * @param {*} không
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
function toggle() {
    inputRef.value.focus();
    hoveredIndex.value = -1;
    show.value = !show.value;
    window.addEventListener('click', clickOutSideCombobox);
    rotateImage();
}

/**
 * Chức năng: Cập nhật giá trị ô input sau khi select, binding 2 chiều valueLocal
 * @param {*} number
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
function handleChangeModelValue(val) {
    let inputVal = '';
    inputVal = val;
    valueLocal.value = inputVal;
}

/**
 * Chức năng: Chuyển đổi trạng thái selection box, cập nhật trạng thái input cha binding 2 chiều,
 * update selection state 1 từ state 2
 * @param {*} number
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
const resetSearchValue = async (item, index) => {
    console.log(`file combobox dong 195: ${item}`);
    hoveredIndexSub.value = -1;
    emit('setId', item[`${props.typeField}Id`]);
    if (props.typeField == 'Department' || props.typeField == 'TypeProperty') {
        valueLocal.value = item[`${props.typeField}Code`];
        selected.value = item;
        emit('setName', {
            type: `${props.typeField}`,
            value: selected.value[`${props.typeField}Name`],
        });
    } else {
        //cho cái combobox của thay đổi page size
        valueLocal.value = item.value;
    }
    /**
     * Mapping từ sub dropdown sang father, cập nhật lại item đang đc select ở father
     */

    var indexMappingFromSub = findIndexByAttribute(
        props.dataList,
        `${props.typeField}Code`,
        item[`${props.typeField}Code`],
    );
    selectDropdownIndex.value = indexMappingFromSub;
    /**
     * Cập nhật lại
     */
    search.value = '';
    state.value = BEnum.COMBO_BOX.all_selection;
    inputRef.value.focus();
    show.value = false;
    isRotating.value = false;
};

/**
 * Chức năng: Xử lý sự kiện nhập dữ liệu vào trong input father cha.
 * @param {*} number
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
async function handleChangeInputFather(el) {
    const result = el.target.value.split(', ');
    //Cập nhật giá trị search khi thay đổi input father
    search.value = result[result.length - 1];

    //Cập nhật lại trạng thái combo box, sổ ra combo box con
    state.value = BEnum.COMBO_BOX.auto_fill;

    // Hiển thị combo box con
    show.value = true;

    //Xoá thông báo lỗi khi validate input
    isDisplayErrorMessage.value = false;
    await nextTick();

    //Đặt chú ý vào input con khi input cha bị ẩn đi rồi
    inputRefSub.value.focus();
}
const onClickFatherSelection = async (item, index) => {
    selectDropdownIndex.value = index;
    hoveredIndex.value = index;
    show.value = false;
    isRotating.value = false;
    await nextTick();
    //Cập nhật id để lưu vào database tương ứng với id selection item
    emit('setId', item[`${props.typeField}Id`]);
    if (props.typeField == 'Department' || props.typeField == 'TypeProperty') {
        valueLocal.value = item[`${props.typeField}Code`];
        selected.value = item[`${props.typeField}Code`];
        emit('setName', {
            type: `${props.typeField}`,
            value: item[`${props.typeField}Name`],
        });
    } else {
        valueLocal.value = item.value;
    }

    // inputRef.value.focus();
};

/**
 * Chức năng: Xử lý việc rotate mũi tên của combo box
 * @param {*} number
 * @returns không
 * Author: lhhanh (27/08/2023)
 */
const isRotating = ref(false);

function rotateImage() {
    isRotating.value = !isRotating.value;
}
</script>
<template>
    <div class="wrapper" ref="inputRefCombobox">
        <label v-if="label">
            <div class="wrapper-dropdown form__div--labelInput">
                {{ label }}
            </div>
            <span class="red-text"> *</span>
        </label>
        <!-- dropdown cho thanh footer của table -->
<transition name="slide" v-if="state === BEnum.COMBO_BOX.all_selection && typeField == 'paging'">
            <div v-if="show" class="content">
                <div class="checkboxes">
                    <div class="inner-wrap">
                        <label v-for="(item, index) in properties" :class="{
                            'background-option tickbox checked':
                                selectDropdownIndex == index,
                            'background-option': item == properties[hoveredIndex],
                        }" @click="onClickFatherSelection(item, index)">
                            <span class="inner-warp--span" style="left: 50%">{{ item[`${typeField}Code`] }}
                            </span>
                        </label>
                    </div>
                </div>
            </div>
        </transition>
<!-- End: dropdown cho thanh footer của table -->

<div class="form-control toggle-next ellipsis form-input-name-row">
            <!-- Begin: Input  -->
            <div class="position-relative">
                <div>
                    <div class="square-24-24-left">
                        <!-- <span class="sprite-filter sprite-center"></span> -->
                        <slot name="icon-name"> </slot>
                    </div>
<input type="text" :class="{
                        'filter ': typeField != 'paging' && errorText == '',
                        'rectangle-25-72': typeField == 'paging',
                        'error-input filter':
                            errorText != '' &&
                            label != undefined &&
                            isDisplayErrorMessage == true,
                    }" :placeholder="placeholder" v-model="valueLocal" @input="handleChangeInputFather"
    v-show="state == BEnum.COMBO_BOX.all_selection" @keydown.stop="onKeyPressAll" ref="inputRef" :readonly="isReadonly"
    :tabindex="tabIndex" @blur="handleBlurInput" />
<!-- Hiển thị một ô input mới, để thao tác phím và data trong combo box mới  -->
<input type="text" :placeholder="placeholder" v-model="valueLocal" @input="handleChangeInputFather"
    v-show="state == BEnum.COMBO_BOX.auto_fill" @keydown="onKeyPressSub" ref="inputRefSub" :readonly="isReadonly"
    :tabindex="tabIndex" @blur="handleBlurInput" :class="{
                            'filter ': typeField != 'paging' && errorText == '',
                            'rectangle-25-72': typeField == 'paging',
                            'error-input filter':
                                errorText != '' &&
                                label != undefined &&
                                isDisplayErrorMessage == true,
                        }" />

<div @click="toggle" class="square-24-24-right input__transform--dropdown center-vertical">
                        <span :class="{
                            'sprite-dropdown sprite-center': isRotating == false,
                            'sprite-dropdown sprite-center  rotating':
                                isRotating == true,
                        }">
                        </span>
                    </div>
<span class="error-message">{{ errorText }}</span>
</div>
</div>
<!-- End: Input  -->
</div>

<!-- Begin: Dropdown autofill cho typeField == 'Department' || typeField == 'TypeProperty' || typeField == 'SearchProperty' -->
<transition name="slide" v-if="
            state === BEnum.COMBO_BOX.all_selection &&
            (typeField == 'Department' ||
                typeField == 'TypeProperty' ||
                typeField == 'SearchProperty')
        ">
            <div v-if="show">
                <div class="checkboxes" id="Lorems">
                    <div class="inner-wrap">
                        <label v-for="(item, index) in properties" :class="{
                            'background-option tickbox checked':
                                selectDropdownIndex == index,
                            'background-option': item == properties[hoveredIndex],
                        }" @click="onClickFatherSelection(item, index)">
                            <span class="inner-warp--span">{{ item[`${typeField}Name`] }}
                            </span>
                        </label>
                    </div>
                </div>
            </div>
        </transition>

<transition name="slide" v-if="state === BEnum.COMBO_BOX.auto_fill">
            <div v-if="show">
                <div class="checkboxes" id="Lorems">
                    <div class="inner-wrap">
                        <label v-for="(item, index) in subdFilteredProperties" :class="{
                            'background-option tickbox checked':
                                selectDropdownIndexSub == index,
                            'background-option': item == properties[hoveredIndexSub],
                        }">
                            <span @click="resetSearchValue(item, index)" class="inner-warp--span">{{
                                item[`${typeField}Name`] }}
                            </span>
                        </label>
                    </div>
                </div>
            </div>
        </transition>
<!-- END: Dropdown autofill cho typeField == 'Department' || typeField == 'TypeProperty' -->
</div>
</template>
<style>
@import url('@/css/components/popup.css');
@import url('@/css/main.css');

.toggle-next {
    border-radius: 0;
}

.checkboxes label {
    cursor: pointer;
    width: 100%;
    display: flex;
    align-items: center;
    background-color: #f9fafc;
}

.inner-wrap label:hover {
    background-color: #c7e0f5;
    border-radius: var(--button-border-radius);
}

.tickbox::before {
    content: '✔';
    color: green;
    opacity: 0;
    transition: opacity 0.2s;
}

.tickbox.checked::before {
    opacity: 1;
}

.background-option {
    background-color: #c7e0f5 !important;
    border-radius: var(--button-border-radius);
    font-size: 14px;
    font-weight: normal;
    font-family: 'Roboto', sans-serif;
}

.ellipsis {
    text-overflow: ellipsis;
    width: 100%;
    white-space: nowrap;
}

.apply-selection {
    display: none;
    width: 100%;
    margin: 0;
    padding: 5px 10px;
    border-bottom: 1px solid #ccc;
}

.apply-selection .ajax-link {
    display: none;
}

.checkboxes {
    /* margin: 0;
  display: block;
  border: 1px solid #ccc;
  border-top: 0; */

    margin: 0;
    display: block;
    border: 1px solid #ccc;
    border-top: 0;
    position: absolute;
    z-index: 10;
    width: 100%;
}

.checkboxes .inner-wrap {
    position: relative;
    padding: 0px 5px;
    max-height: 140px;
    overflow: auto;
    border-radius: var(--button-border-radius);
    box-shadow: var(--button-hover-effect);
}

.slide-enter-active,
.slide-leave-active {
    transition: 400 3s ease;
}

.slide-enter-from,
.slide-leave-to {
    max-height: 0 !important;
}

.ckkBox.val {
    margin: 0px 0.5rem 0px 0px;
    width: 1rem !important;
    height: 1rem;
}

.inner-wrap {
    background-color: white;
}

.inner-wrap label {
    display: flex;
    align-items: center;
    margin-bottom: 0.25rem;
    margin-top: 0.25rem;
    font-size: 14px;
    font-weight: normal;
    font-family: 'Roboto', sans-serif;
    padding: 12px;
    height: 19px;
}

.inner-warp--span {
    position: absolute;
    /* left: 40px; */
    left: 20%;
}

.wrapper {
    position: relative;
}

.wrapper .content {
    display: flex;
    flex-direction: column-reverse;
    top: -114px;
}

.rotating {
    transform: rotate(180deg) !important;
    transform-origin: 25% 25% !important;
}

.width-100 {
    width: 100%;
}
</style> -->
