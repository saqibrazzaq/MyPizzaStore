export default class CitySearchRequestParams {
  stateId?: string;
  searchText?: string;

  constructor(stateId?: string, searchText?: string) {
    this.stateId = stateId;
    this.searchText = searchText;
  }
}