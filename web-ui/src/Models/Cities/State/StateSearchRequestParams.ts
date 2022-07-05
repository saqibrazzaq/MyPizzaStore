export default class StateSearchRequestParams {
  countryId?: string;
  searchText?: string;

  constructor(countryId?: string, searchText?: string) {
    this.countryId = countryId;
    this.searchText = searchText;
  }
}