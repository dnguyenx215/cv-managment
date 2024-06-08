<script setup>
const mailData = defineModel('mailData')

const salaryComputed = computed({
    get: () => {
        var input = mailData.value.Salary.replace(/[\D\s\._\-]+/g, "");
        input = input ? parseFloat(input, 10) : 0;
        var res = input.toLocaleString("en-US")
        //mailData.value.Salary = res
        return res
    },
    set: (newValue) => {
        newValue = newValue.replace(/[\D\s\._\-]+/g, "");
        newValue = newValue ? parseFloat(newValue, 10) : 0;
        let isNumber = !isNaN(parseFloat(newValue)) && isFinite(newValue)

        //edge case when clear number
        if (newValue == '') {
            mailData.value.Salary = ''
        }

        if (isNumber && parseFloat(newValue) > 0) {
            mailData.value.Salary = newValue.toLocaleString("en-US")
        }

    }
})

/**
 * Reset input value after computed execute if it's not number
 * @param {*} event
 */
const handleInput = (event) => {
    const inputValue = event.target.value
    const isNumber = !isNaN(parseFloat(inputValue)) && isFinite(inputValue)

    if (!isNumber) {
        event.target.value = salaryComputed.value
    }
}
</script>
<template>
    <input id="amount" name="amount" type="text" maxlength="15" v-model="salaryComputed" class="form-control"
        placeholder="Lương" aria-label="Lương" @input="handleInput" />
</template>

<style></style>
