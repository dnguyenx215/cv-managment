import CV_CONSTANT from '~/assets/js/constants/constants'
import { format, parse } from 'date-fns';
import BEnum from '~/assets/js/enum/BEnum';


/**
 * Handle yyyy-MM-dd format dates
 * @param {*} date
 */
const formatDate = (date) => {
    return format(new Date(date), "dd/MM/yyyy");
};
/**
 * Handle yyyy-MM-dd format dates
 * @param {*} date
 */
const formatDateYYYYmmdd = (date) => {
    if (typeof date !== 'string') {
        return 'Invalid Date';
    }
    const dateObj = new Date(date);
    if (isNaN(dateObj.getTime())) {
        return 'Invalid Date';
    }
    return format(dateObj, 'yyyy-MM-dd');
};
const formatDateWithTime = (date) => {
    return format(new Date(date), "dd/MM/yyyy hh:mm a");
};
const formatTime = (time) => {
    if (time != 'null') {
        return format(parse(time.split(":", 2).join(":"), "HH:mm", new Date()), "hh:mm a")
    }
}

function getKeyByValue(object, value) {
    var res = Object.keys(object).find(key => object[key] == value);
    return res
}



function splitArrayByStatusCV(data) {
    const result = {};

    data.forEach(obj => {
        const { status } = obj;

        if (!result[status]) {
            result[status] = [];
        }

        result[status].push(obj);
    });

    return result;
}
function capitalize(s) {
    if (!s) return s;
    // return s && s[0].toUpperCase() + s.slice(1);
}
function getVietNameseText(constant) {
    const CV_CONSTANT_VN = {
        REVIEW_CV_WAITING: "Đang chờ xem xét hồ sơ",
        REVIEW_CV_FAIL: "Không đủ điều kiện",
        REVIEW_CV_PASS: "Đủ điều kiện",
        REFUSED_TO_BE_INTERVIEWED: "Từ chối phỏng vấn",
        INTERVIEW_RES_FAIL: "Không đạt phỏng vấn",
        INTERVIEW_RES_PASS: "Đạt phỏng vấn",
        REFUSE_TO_ACCEPT_JOB: "Từ chối nhận việc",
        GET_JOB: "Đã nhận việc",
        INTERVIEW_RES_BACKUP: "Kết quả phỏng vấn dự phòng",
        NOT_SENT: "Chưa gửi",
        SENDING: "Đang gửi",
        SENT: "Đã gửi"
    };

    return CV_CONSTANT_VN[constant] || constant;
}



function getAllowedUpdateStatus(currentStatus) {
    if (currentStatus) {
        switch (currentStatus) {
            case CV_CONSTANT.REVIEW_CV_WAITING:
                return [
                    CV_CONSTANT.REVIEW_CV_FAIL,
                    CV_CONSTANT.REVIEW_CV_PASS,

                ];
            case CV_CONSTANT.REVIEW_CV_FAIL:
                return [currentStatus];
            case CV_CONSTANT.REVIEW_CV_PASS:
                return [CV_CONSTANT.INTERVIEW_RES_FAIL, CV_CONSTANT.INTERVIEW_RES_PASS, CV_CONSTANT.REVIEW_CV_PASS];
            case CV_CONSTANT.INTERVIEW_RES_PASS:
                return [CV_CONSTANT.INTERVIEW_RES_BACKUP, CV_CONSTANT.INTERVIEW_RES_PASS];
            case CV_CONSTANT.INTERVIEW_RES_FAIL:
                return [currentStatus];
            case CV_CONSTANT.INTERVIEW_RES_BACKUP:
                return [CV_CONSTANT.INTERVIEW_RES_FAIL, CV_CONSTANT.INTERVIEW_RES_PASS, CV_CONSTANT.INTERVIEW_RES_BACKUP];
            default:
                return [];
        }
    }
    else return CV_CONSTANT
}

function getAllowedDisplayTimeAndDateInterview(currentStatus) {
    if (currentStatus) {
        switch (currentStatus) {
            case CV_CONSTANT.REVIEW_CV_WAITING:
                return true
            case CV_CONSTANT.REVIEW_CV_FAIL:
                return false;
            case CV_CONSTANT.REVIEW_CV_PASS:
                return true
            case CV_CONSTANT.INTERVIEW_RES_PASS:
                return true;
            case CV_CONSTANT.INTERVIEW_RES_FAIL:
                return false;
            case CV_CONSTANT.INTERVIEW_RES_BACKUP:
                return false;
            default:
                return [];
        }
    }
    else return CV_CONSTANT
}
function checkIfAllowedUpdateStatus(currentStatus, status) {
    return getAllowedUpdateStatus(currentStatus).includes(status);
}

// {
//     columnLayoutId: 1,
//     columnLayoutName: 'Waiting review',
//     putColumns: [],
//     pullColumns: ['Review failed', 'Review pass']
// },
function convertColumnRelationship(data) {
    const result = data.reduce((acc, item) => {
        const existingLayout = acc.find(layout => layout.columnLayoutId === item.columnLayoutId);
        if (existingLayout) {
            if (item.pullColumnId !== null) {
                existingLayout.pullColumns.push(item.pullColumn.columnName);
            }
            if (item.putColumnId !== null) {
                existingLayout.putColumns.push(item.putColumn.columnName);
            }
        } else {
            acc.push({
                columnLayoutId: item.columnLayoutId,
                columnLayoutName: item.columnOwnRelationship.columnName,
                putColumns: item.putColumnId !== null ? [item.putColumn.columnName] : [],
                pullColumns: item.pullColumnId !== null ? [item.pullColumn.columnName] : [],
            });
        }
        return acc;
    }, []);
}

const getBackgroundColor = (status) => {
    switch (status) {
        case CV_CONSTANT.NOT_SENT:
            return "badge p-1 text-bg-info";
        case CV_CONSTANT.SENDING:
            return "badge p-1 text-bg-warning";
        case CV_CONSTANT.SENT:
            return "badge p-1 text-bg-success";
        default:
            return "gray";
    }
}


function getConstantFromColumnId(columnId) {
    const columnLayout = BEnum.COLUMN_LAYOUT;
    switch (columnId) {
        case columnLayout.review_cv_waiting:
            return CV_CONSTANT.REVIEW_CV_WAITING;
        case columnLayout.review_cv_pass:
            return CV_CONSTANT.REVIEW_CV_PASS;
        case columnLayout.review_cv_fail:
            return CV_CONSTANT.REVIEW_CV_FAIL;
        case columnLayout.interview_res_pass:
            return CV_CONSTANT.INTERVIEW_RES_PASS;
        case columnLayout.interview_res_fail:
            return CV_CONSTANT.INTERVIEW_RES_FAIL;
        case columnLayout.interview_res_backup:
            return CV_CONSTANT.INTERVIEW_RES_BACKUP;
        default:
            return columnId;
    }
}

function findById(id, array) {
    return array.find(item => item.id === id);
}

// Remove an element from an array by id
function removeById(id, array) {
    return array.filter(item => item.id !== id);
}

// Update an element in an array by id
function updateById(id, array, updatedItem) {
    return array.map(item => {
        if (item.id === id) {
            return { ...item, ...updatedItem };
        }
        return item;
    });
}

/**
 * tìm kiếm vị trí của 1 giá trị
 * @param {Array} arrayFind mảng cần tìm kiếm
 * @param {string} attr thuộc tính cần tìm kiếm
 * @param {*} valueFind giá trị cần tìm kiếm
 * @returns -1 nếu không tìm thấy, 1 số >= 0 nếu tìm thấy
 */
function findIndexByAttribute(arrayFind, attr, valueFind) {
    try {
        if (attr === "" || attr == '') {
            for (let i = 0; i < arrayFind.length; i++) {
                if (arrayFind[i] == valueFind) {
                    return i;
                }
            }
        } else {
            for (let i = 0; i < arrayFind.length; i++) {
                if (arrayFind[i][attr] == valueFind) {
                    return i;
                }
            }
        }

        return -1;
    } catch (error) {
        console.log("🚀 ~ file: helper.js:5 ~ findIndexByAttribute ~ error:", error);
    }
};
//    const myString = "apple, banana, orange, kiwi";
//    const myArray = myString.split(/\s*,\s*/).filter(Boolean);
function isNonEmptyString(value) {
    return typeof value === 'string' && value.length > 0;
}

function getRandomBackgroundColor() {
    const lightColors = [
        '#F5F5F5', // Light Gray
        '#FFFAFA', // Snow
        '#FFF9C4', // Light Yellow
        '#E0FFFF', // Light Cyan
        '#FFF0F5', // Light Pink
        '#E6E6FA', // Light Lavender
        '#D8BFD8', // Light Thistle
        '#FFEBCD', // Blanched Almond
        '#F0FFF0', // Honeydew
        '#F5FFFA', // Mint Cream
        '#FAEBD7', // Antique White
        '#FAF0E6'  // Linen
    ];

    const randomIndex = Math.floor(Math.random() * lightColors.length);
    return lightColors[randomIndex];
}

function useDebounce(callback, delay) {

    let timeout = null

    return (() => {
        clearTimeout(timeout)

        timeout = setTimeout(() => {
            callback()
        }, delay)
    })
}
/**
 * hàm tạo ra uuid
 * @author: Lhha
 * @returns 1 uuid mới
 */
function generateUuid() {
    try {
        return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (c) =>
            (
                c ^
                (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
            ).toString(16)
        );
    } catch (error) {
        console.log("🚀 ~ file: helper.js:5 ~ findIndexByAttribute ~ error:", error);
    }
};


/**
 * Extracts and returns the Gmail address from a given string.
 *
 * @param {string} stringContainGmail - The input string that may contain a Gmail address.
 * @return {string} The extracted Gmail address, or an empty string if not found.
 */
const extractGmailOnly = (stringContainGmail) => {
    let pattern = /\b[\w.]+@[\w.]+\.\w+\b/;
    let match = stringContainGmail.match(pattern);

    if (match) {
        let email = match[0];
        return email

    } else {
        console.log("Email not found.");
        return "";
    }
}
const extractFromToAddressFromEmailObject = (object) => {
    const data = [
        {
            "from": "Google <no-reply@accounts.google.com>",
            "to": "Google <no-reply@accounts.google.com>"
        }
    ];
    var arrayRes = [];
    object.forEach((obj) => {
        arrayRes.push(extractGmailOnly(obj.from))
        arrayRes.push(extractGmailOnly(obj.to))
    });
    return arrayRes
}
function isNotNumber(event) {
    // Get the key code
    const keyCode = event.keyCode || event.which;

    // Check if the key code is outside the range of numeric keys
    return (keyCode < 48 || keyCode > 57) && (keyCode < 96 || keyCode > 105);
}

/**
 * Checks if two objects are equal by comparing their string representations.
 *
 * @param {object} obj1 - The first object to compare. aka mailtemplateData.data.Content
 * @param {object} obj2 - The second object to compare. aka newTemplateEmail.Content
 * @return {boolean} Returns true if the objects are equal, false otherwise.
 */
function areObjectsEqual(obj1, obj2) {
    const obj1String = JSON.stringify(obj1);
    const obj2String = JSON.stringify(obj2);

    return obj1String == obj2String;
};
export { areObjectsEqual, isNotNumber, extractFromToAddressFromEmailObject, extractGmailOnly, formatDateYYYYmmdd, generateUuid, useDebounce, getRandomBackgroundColor, isNonEmptyString, findIndexByAttribute, removeById, updateById, findById, getConstantFromColumnId, getBackgroundColor, convertColumnRelationship, formatDateWithTime, getAllowedDisplayTimeAndDateInterview, formatTime, formatDate, getKeyByValue, splitArrayByStatusCV, capitalize, getVietNameseText, getAllowedUpdateStatus, checkIfAllowedUpdateStatus }
