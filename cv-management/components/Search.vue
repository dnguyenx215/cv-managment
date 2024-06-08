<template>
    <form action="">
        <div class="mt-3">Tìm kiếm:</div>
        <div class="search-text d-flex">
            <div class="col-md-3">
                <input v-model="search" class="form-control col-md-2" type="text" placeholder="Nhập... " />
            </div>
            <span class="button d-flex">
                <button class="ms-2 me-1 btn btn-primary" type="submit">
                    <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24">
                        <path fill="currentColor"
                            d="M15.5 14h-.79l-.28-.27A6.471 6.471 0 0 0 16 9.5A6.5 6.5 0 1 0 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5S14 7.01 14 9.5S11.99 14 9.5 14" />
                    </svg>Tìm kiếm
                </button>
                <button class="btn btn-outline-primary" type="button" @click="clear">
                    <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 256 256">
                        <path fill="currentColor"
                            d="M225 80.4L183.6 39a24 24 0 0 0-33.94 0L31 157.66a24 24 0 0 0 0 33.94l30.06 30.06a8 8 0 0 0 5.68 2.34H216a8 8 0 0 0 0-16h-84.7l93.7-93.66a24 24 0 0 0 0-33.94M213.67 103L160 156.69L107.31 104L161 50.34a8 8 0 0 1 11.32 0l41.38 41.38a8 8 0 0 1 0 11.31Z" />
                    </svg>
                    Xóa Form
                </button>
            </span>
        </div>
    </form>
</template>
<script setup>
    const search = ref('');
    const clear = async () => {
        search.value = '';
    };
</script>
