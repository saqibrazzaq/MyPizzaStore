import HrRequestParameters from "../HrRequestParameters";

export default class DeleteCompanyRequestParams extends HrRequestParameters {
  companyId: string;
  constructor(companyId:string, accountId?: string) {
    super(accountId);

    this.companyId = companyId;
  }
}