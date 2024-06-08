import CV_CONSTANT from '~/assets/js/constants/constants'
import { format } from 'date-fns';
import { isNonEmptyString } from './helper';
function isArrayEmpty(arr) {
    var errors = []
    // Check if the input is an array
    if (!Array.isArray(arr)) {
        errors.push('Input is not an array.');
    }
    // Check if the array length is zero
    return arr.length === 0;
}
function validateCvInfoBeforeSendingMail(data) {
    var errors = []


    // For DateOfInterview dateOfInterview,
    if (data.status == CV_CONSTANT.REVIEW_CV_PASS) {
        if (!isNonEmptyString(data.dateOfInterview)) {
            errors.push('Invalid date of interview');
            return errors
        }
    }

    // For TimeOfInterview timeOfInterview
    if (data.status == CV_CONSTANT.REVIEW_CV_PASS) {
        if (!isNonEmptyString(data.timeOfInterview)) {
            errors.push('Invalid time of interview');
            return errors
        }
    }

    // For DateAcceptJob
    if (data.status == CV_CONSTANT.INTERVIEW_RES_PASS) {
        if (!isNonEmptyString(data.dateAcceptJob)) {
            errors.push('Invalid date accept job');
            return errors
        }
    }
    return errors
}

/**
 * Handle yyyy-MM-dd format dates
 * @param {*} date
 */
function formatDateYYYYmmdd(date) {
    if (typeof date !== 'string') {
        return 'Invalid Date';
    }
    const dateObj = new Date(date);
    if (isNaN(dateObj.getTime())) {
        return 'Invalid Date';
    }
    return format(dateObj, 'yyyy-MM-dd');
};

export { isArrayEmpty, formatDateYYYYmmdd, validateCvInfoBeforeSendingMail }
