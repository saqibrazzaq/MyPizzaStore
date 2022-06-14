import { Flex, Stack } from "@chakra-ui/react";
import React from "react";
import { Outlet } from "react-router-dom";
import AdminSidebar from "./AdminSidebar";

const AdminLayout = () => {
  return (
    <Stack minH={"50vh"} direction={{ base: "column", md: "row" }}>
      <Flex p={0} flex={0} align={"center"} justify={"center"}>
        <AdminSidebar children="" />
      </Flex>
      <Flex flex={1}>
        <Outlet />
      </Flex>
    </Stack>
  );
};

export default AdminLayout;
