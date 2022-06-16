import {
  Alert,
  AlertDescription,
  AlertIcon,
  AlertTitle,
  Box,
  Button,
  Container,
  Heading,
  Icon,
  Stack,
  Text,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import UserDto from "../../Models/User/UserDto";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { BsCheckCircle } from "react-icons/bs";
import ErrorDetails from "../../Models/Error/ErrorDetails";

const VerifyAccount = () => {
  const axiosPrivate = useAxiosPrivate();
  const [user, setUser] = useState<UserDto>();
  const [error, setError] = useState("");

  useEffect(() => {
    loadUserInfo();
  }, []);

  const loadUserInfo = () => {
    setError("");
    axiosPrivate
      .get("User/info")
      .then((res) => {
        // console.log(res);
        setUser(res.data);
      })
      .catch((err) => {
        // console.log(err);
        let errDetails: ErrorDetails = err?.response?.data;
        setError(errDetails?.Message || "Service failed.");
      });
  };

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        <Heading fontSize={"xl"}>Verify Account</Heading>
        {user?.emailConfirmed ? (
          <Alert status="success">
            <AlertIcon />
            <AlertTitle>Account verified</AlertTitle>
            <AlertDescription></AlertDescription>
          </Alert>
        ) : (
          <Box>
            <Alert status="error">
              <AlertIcon />
              <AlertTitle>Account is not verified</AlertTitle>
              <AlertDescription>{error}</AlertDescription>
            </Alert>

            <Button colorScheme="blue" mt={4} >Send Verification Email</Button>
          </Box>
        )}
      </Stack>
    </Box>
  );
};

export default VerifyAccount;
