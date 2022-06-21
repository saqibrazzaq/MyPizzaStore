import {
  Alert,
  AlertDescription,
  AlertIcon,
  AlertTitle,
  Box,
  Container,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Heading,
  Image,
  Input,
  shouldShowFallbackImage,
  Stack,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import Common from "../../utility/Common";
import { Field } from "formik";
import UserDto from "../../Models/User/UserDto";
import useAxiosAuth from "../../hooks/useAxiosAuth";
import ErrorDetails from "../../Models/Error/ErrorDetails";

const ProfilePicture = () => {
  const [error, setError] = useState("");
  const [image, setImage] = useState(Common.DEFAULT_PROFILE_PICTURE);
  const axiosPrivate = useAxiosAuth();

  useEffect(() => {
    loadUserInfo();
  }, [image]);

  const loadUserInfo = () => {
    setError("");
    axiosPrivate
      .get("User/info")
      .then((res) => {
        console.log(res);
        if(res.data.profilePictureUrl) {
          setImage(res.data.profilePictureUrl);
        }
      })
      .catch((err) => {
        // console.log(err);
        let errDetails: ErrorDetails = err?.response?.data;
        setError(errDetails?.Message || "Service failed.");
      });
  };

  const showError = () => (
    <Alert status="error">
      <AlertIcon />
      <AlertTitle>Error</AlertTitle>
      <AlertDescription>{error}</AlertDescription>
    </Alert>
  );

  const showImage = () => <Image boxSize="200px" src={image} />;

  const handleSubmit = () => {};

  const showUploadForm = () => (
    <form onSubmit={handleSubmit}>
      <FormControl>
        <FormLabel htmlFor="username">Username</FormLabel>
        <Field as={Input} id="username" name="username" type="file" />
      </FormControl>
    </form>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        <Heading fontSize={"xl"}>User Profile Picture</Heading>
        {error && showError()}
        {image && showImage()}
      </Stack>
    </Box>
  );
};

export default ProfilePicture;
