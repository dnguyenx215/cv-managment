import Api from "./Base/api";

export default class ColumnRelationshipApi extends Api {
    getAll(params: any) {
        return this.get('CV', params);
    }
};
