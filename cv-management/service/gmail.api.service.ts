import Api from "./Base/api";

export default class GmailApi extends Api {

    getAllEmails(params: any, config: any) {
        return this.postAPI(`Mail/GetAllMails`, config);
    }
};
