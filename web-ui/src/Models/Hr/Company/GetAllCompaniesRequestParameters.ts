import HrRequestParameters from "../HrRequestParameters";

export default class GetAllCompaniesRequestParameters 
extends HrRequestParameters {
   
  constructor(accountId?:string) {
    super(accountId);
  }
}