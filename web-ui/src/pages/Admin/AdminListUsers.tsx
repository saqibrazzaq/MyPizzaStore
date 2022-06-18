import { DEFAULT_PAGE_SIZE } from "../../utility/Common";
import {
  Box,
  Button,
  Container,
  Heading,
  IconButton,
  Link,
  Stack,
  Table,
  TableCaption,
  TableContainer,
  Tbody,
  Td,
  Tfoot,
  Th,
  Thead,
  Tr,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import PagedResponse from "../../Models/PagedResponse";
import { Link as RouteLink } from "react-router-dom";
import UserDto from "../../Models/User/UserDto";
import { MdDeleteOutline } from "react-icons/md";
import { AiFillDelete } from "react-icons/ai";
import SearchUsersRequestParameters from "../../Models/User/SearchUsersRequestParameters";
import DeleteIconButton from "../../components/Buttons/DeleteIconButton";

const AdminListUsers = () => {
  const [pagedRes, setPagedRes] = useState<PagedResponse<UserDto>>();
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    searchUsers(new SearchUsersRequestParameters("", 1, DEFAULT_PAGE_SIZE, ""));
  }, []);

  const previousPage = () => {
    if (pagedRes?.metaData) {
      let previousPageNumber = (pagedRes?.metaData?.currentPage || 2) - 1;
      let searchParams = new SearchUsersRequestParameters(
        "",
        previousPageNumber,
        DEFAULT_PAGE_SIZE,
        ""
      );

      searchUsers(searchParams);
    }
  };

  const nextPage = () => {
    if (pagedRes?.metaData) {
      let nextPageNumber = (pagedRes?.metaData?.currentPage || 0) + 1;
      let searchParams = new SearchUsersRequestParameters(
        "",
        nextPageNumber,
        DEFAULT_PAGE_SIZE,
        ""
      );

      searchUsers(searchParams);
    }
  };

  const searchUsers = (searchParams: SearchUsersRequestParameters) => {
    axiosPrivate
      .get("User/search-users", {
        params: searchParams,
      })
      .then((res) => {
        let userRes: PagedResponse<UserDto> = res.data;
        // console.log(userRes.metaData);
        setPagedRes(userRes);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        <Heading fontSize={"xl"}>Search Users</Heading>
        <TableContainer>
          <Table variant="simple">
            <Thead>
              <Tr>
                <Th>Username</Th>
                <Th>Email</Th>
                <Th></Th>
              </Tr>
            </Thead>
            <Tbody>
              {pagedRes?.pagedList ? (
                pagedRes.pagedList.map((item) => (
                  <Tr key={item.email}>
                    <Td>{item.userName}</Td>
                    <Td>{item.email}</Td>
                    <Td>
                      <Link as={RouteLink} to={"/admin/users/delete/" + item.userName}>
                      <DeleteIconButton />
                      </Link>
                    </Td>
                  </Tr>
                ))
              ) : (
                <></>
              )}
            </Tbody>
            <Tfoot>
              <Tr>
                <Th colSpan={3} textAlign="center">
                  <Button
                    isDisabled={!pagedRes?.metaData?.hasPrevious}
                    variant="link"
                    mr={5}
                    onClick={previousPage}
                  >
                    Previous
                  </Button>
                  Page {pagedRes?.metaData?.currentPage} of{" "}
                  {pagedRes?.metaData?.totalPages}
                  <Button
                    isDisabled={!pagedRes?.metaData?.hasNext}
                    variant="link"
                    ml={5}
                    onClick={nextPage}
                  >
                    Next
                  </Button>
                </Th>
              </Tr>
            </Tfoot>
          </Table>
        </TableContainer>
      </Stack>
    </Box>
  );
};

export default AdminListUsers;
