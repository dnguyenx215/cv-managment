import Api from "./Base/api";

export default class CvApi extends Api {
    getAllCv(params: any) {
        return this.get('CV', params);
    }

    //CvDTO contain MailContent model
    sendMailBaseOnStatus(idCvNeedUpdate: number, data: any) {
        return this.putAPI(`CV/sendmail?idCvNeedUpdate=${idCvNeedUpdate}`, data);
    }

    //[CvDTO contain MailContent model]
    sendMailssBaseOnStatus(data: any) {
        return this.putAPI(`CV/send-list-mail?`, data);
    }

    updateCV(params: number, data: any) {
        return this.putAPI(`CV/${params}`, data);
    }

    /**
     *
     * @param params - REVIEW_CV_FAIL, REVIEW_CV_WAITING...
     * @param data
     * @returns
     */
    getEmailTemplate(params: string, data: any) {
        return this.postAPI(`CV/get-email-template?StatusCv=${params}`, data);
    }
};
