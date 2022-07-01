import {
  Box,
  Container,
  Heading,
  Link,
  Stack,
  Table,
  TableContainer,
  Tbody,
  Td,
  Tfoot,
  Th,
  Thead,
  Tr,
} from "@chakra-ui/react";
import { config } from "dotenv";
import React, { useEffect, useState } from "react";
import HrApi from "../../../Api/HrApi";
import DeleteIconButton from "../../../components/Buttons/DeleteIconButton";
import useAuth from "../../../hooks/useAuth";
import CompanyResponseDto from "../../../Models/Hr/Company/CompanyResponseDto";
import GetAllCompaniesRequestParameters from "../../../Models/Hr/Company/GetAllCompaniesRequestParameters";
import AuthModel from "../../../Models/User/AuthModel";
import { Link as RouteLink } from "react-router-dom";
import UpdateIconButton from "../../../components/Buttons/UpdateIconButton";

const AdminListCompanies = () => {
  const { auth }: AuthModel = useAuth();
  const getAllParams = new GetAllCompaniesRequestParameters(auth.accountId);

  const [companies, setCompanies] = useState<CompanyResponseDto[]>();

  useEffect(() => {
    loadCompanies();
  }, []);

  const loadCompanies = () => {
    HrApi.get("Companies", {
      params: getAllParams,
    })
      .then((res) => {
        // console.log(res.data);
        setCompanies(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const displayCompanies = () => (
    <TableContainer>
      <Table variant="simple">
        <Thead>
          <Tr>
            <Th>Name</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {companies ? (
            companies.map((item) => (
              <Tr key={item.companyId}>
                <Td>{item.name}</Td>
                <Td>
                  <Link mr={2}
                    as={RouteLink}
                    to={"/admin/company/update/" + item.companyId}
                  >
                    <UpdateIconButton />
                  </Link>
                  <Link
                    as={RouteLink}
                    to={"/admin/company/delete/" + item.companyId}
                  >
                    <DeleteIconButton />
                  </Link>
                </Td>
              </Tr>
            ))
          ) : (
            <></>
          )}
        </Tbody>
      </Table>
    </TableContainer>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        <Heading fontSize={"xl"}>Companies</Heading>
        {companies && displayCompanies()}
      </Stack>
    </Box>
  );
};

export default AdminListCompanies;
