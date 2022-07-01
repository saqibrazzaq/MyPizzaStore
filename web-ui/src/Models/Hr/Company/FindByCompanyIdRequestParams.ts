import HrRequestParameters from "../HrRequestParameters";

export default class FindByCompanyIdRequestParams extends HrRequestParameters {
  constructor(accountId?: string) {
    super(accountId);
  }
}