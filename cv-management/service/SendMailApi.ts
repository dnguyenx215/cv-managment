import Api from "./Base/api";

export default class SendMailApi extends Api {
    updateStatusCv(params: any, data: any) {
        // StatusCv=1&idCvNeedUpdate=1
        var endPoint = `CV/update-status?StatusCv=${params.StatusCv}&idCvNeedUpdate=${params.idCvNeedUpdate}&columnId=${params.columnId}`;
        return this.putAPI(endPoint, data);
    }

    updateStatusListCv(params: any, data: Array<any>) {
        // StatusCv=1&idCvNeedUpdate=1
        var endPoint = `CV/update-status?StatusCv=${params.StatusCv}&idCvNeedUpdate=${params.idCvNeedUpdate}&columnId=${params.columnId}`;
        return this.putAPI(endPoint, data);
    }
}
