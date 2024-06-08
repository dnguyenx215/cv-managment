<template>
    <CVModal>
        <template #title>
            <slot name="header"></slot>
        </template>
        <template #body>
            <form>
                <div class="row mb-3 form-group required">
                    <label for="email" class="col-sm-3 col-form-label control-label text-end">Email</label>
                    <div class="col-sm-9">
                        <input type="email" class="form-control" id="email">
                    </div>
                </div>

                <div class="row mb-3 form-group required">
                    <label for="password" class="col-sm-3 col-form-label control-label text-end">Mật khẩu</label>
                    <div class="col-sm-9">
                        <input type="password" class="form-control" id="password">
                    </div>
                </div>

                <div class="row mb-3 form-group required">
                    <label for="retype-password" class="col-sm-3 col-form-label control-label text-end">Nhập lại mật
                        khẩu</label>
                    <div class="col-sm-9">
                        <input type="password" class="form-control" id="retype-password">
                    </div>
                </div>

                <div class="row mb-3 form-group required">
                    <label for="phone" class="col-sm-3 col-form-label control-label text-end">Điện thoại</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" id="phone">
                    </div>
                </div>
            </form>
        </template>
        <template #footer>

        </template>
    </CVModal>
</template>
