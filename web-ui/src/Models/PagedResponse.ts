import UserDto from "./User/UserDto";

interface MetaData {
  totalCount?: number;
  pageSize?: number;
  currentPage?: number;
  totalPages?: number;
}

export default interface PagedResponse {
  pagedList?: UserDto[];
  metaData?: MetaData;
}