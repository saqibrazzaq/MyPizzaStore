import {
  Box,
  Container,
  Heading,
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

const AdminListUsers = () => {
  const [pagedRes, setPagedRes] = useState<PagedResponse>();
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    axiosPrivate
      .get("User/search-users")
      .then((res) => {
        let userRes: PagedResponse = res.data;
        console.log(userRes.pagedList);
        setPagedRes(userRes);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"} textAlign={"center"}>
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
                  <Tr>
                    <Td>{item.userName}</Td>
                    <Td>{item.email}</Td>
                    <Td isNumeric>25.4</Td>
                  </Tr>
                ))
              ) : (
                <p>nothing</p>
              )}
              <Tr>
                <Td>inches</Td>
                <Td>millimetres (mm)</Td>
                <Td isNumeric>25.4</Td>
              </Tr>
            </Tbody>
            <Tfoot>
              <Tr>
                <Th>To convert</Th>
                <Th>into</Th>
                <Th isNumeric>multiply by</Th>
              </Tr>
            </Tfoot>
          </Table>
        </TableContainer>
      </Stack>
    </Box>
  );
};

export default AdminListUsers;
