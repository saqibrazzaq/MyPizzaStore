import {
  Alert,
  AlertDescription,
  AlertDialog,
  AlertDialogBody,
  AlertDialogContent,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogOverlay,
  AlertIcon,
  AlertTitle,
  Box,
  Button,
  Container,
  Heading,
  HStack,
  Link,
  Stack,
  Table,
  TableContainer,
  Tbody,
  Td,
  Text,
  Th,
  Tr,
  useDisclosure,
  useToast,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { Link as RouteLink, useNavigate, useParams } from "react-router-dom";
import BackButton from "../../components/Buttons/BackButton";
import CancelButton from "../../components/Buttons/CancelButton";
import DeleteButton from "../../components/Buttons/DeleteButton";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import ErrorDetails from "../../Models/Error/ErrorDetails";
import DeleteUserDto from "../../Models/User/DeleteUserDto";
import GetUserDto from "../../Models/User/GetUserDto";
import UserDto from "../../Models/User/UserDto";

const DeleteUser = () => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  const [user, setUser] = useState<UserDto>();
  const [error, setError] = useState("");

  const toast = useToast();

  const axios = useAxiosPrivate();
  const navigate = useNavigate();
  let params = useParams();
  const apiParams = new GetUserDto(params.username || "");

  useEffect(() => {
    axios
      .get("User", {
        params: apiParams,
      })
      .then((res) => {
        // console.log(res.data);
        setUser(res.data);
        setError("");
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        setError(errDetails?.Message || "Service failed");
      });
  }, []);

  let data = new DeleteUserDto(apiParams.username);
  const deleteUser = () => {
    onClose();
    axios
      .post("User/delete-user", data)
      .then((res) => {
        // console.log("User deleted successfully.");
        toast({
          title: "User deleted",
          description: apiParams.username + " deleted successfully.",
          status: "error",
          position: "top-right",
        });
        navigate("/admin/users");
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        setError(errDetails?.Message || "Service failed");
      });
  };

  const showError = () => (
    <Alert status="error">
      <AlertIcon />
      <AlertTitle>Error</AlertTitle>
      <AlertDescription>{error}</AlertDescription>
    </Alert>
  );

  const showUserInfo = () => (
    <div>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Username</Th>
              <Td>{user?.userName}</Td>
            </Tr>
            <Tr>
              <Th>Email</Th>
              <Td>{user?.email}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack spacing={4}>
        <Link onClick={onOpen}>
          <DeleteButton text="YES, I WANT TO DELETE THIS USER" />
        </Link>

        <Link as={RouteLink} to="/admin/users">
          <BackButton />
        </Link>
      </HStack>
    </div>
  );

  const showAlertDialog = () => (
    <AlertDialog
      isOpen={isOpen}
      leastDestructiveRef={cancelRef}
      onClose={onClose}
    >
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize="lg" fontWeight="bold">
            Delete User
          </AlertDialogHeader>

          <AlertDialogBody>
            Are you sure? You can't undo this action afterwards.
          </AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <CancelButton />
            </Link>
            <Link onClick={deleteUser} ml={3}>
              <DeleteButton text="Delete User" />
            </Link>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialogOverlay>
    </AlertDialog>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        <Heading fontSize={"xl"}>Delete User</Heading>
        <Text fontSize="xl">
          Are you sure you want to delete the following user?
        </Text>
        {error && showError()}
        {showUserInfo()}
      </Stack>
      {showAlertDialog()}
    </Box>
  );
};

export default DeleteUser;
