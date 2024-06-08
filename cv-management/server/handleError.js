/**
 * Checks the error object for a specific field and returns the error message if it exists.
 *
 * @param {object} errorObject - The error object to check.
 * @param {string} fieldCheckError - The field to check for in the error object.
 * @return {string} The error message if it exists, otherwise undefined.
 */
const checkErrorField = (errorObject, fieldCheckError) => {
    if (errorObject && errorObject[fieldCheckError]) {
        return errorObject[fieldCheckError][0];
    }
}

/**
 * Form validation invalid vee validate
 * @param {*} { values, errors, results }
 */
const onInvalidSubmit = ({ values, errors, results }) => {
    let firstError = Object.keys(errors)[0];

    for (var i = 0; i < Object.keys(values).length; i++) {
        let keyName = Object.keys(values)[i]
        if (Object.keys(errors).includes(keyName)) {
            firstError = keyName
            break;
        }
    }

    const el = document.querySelector(`[name="${firstError}"]`);
    el?.scrollIntoView({
        behavior: 'smooth',
    });
    el.focus();
}
export { checkErrorField, onInvalidSubmit }
