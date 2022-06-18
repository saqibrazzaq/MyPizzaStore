import { IconButton } from "@chakra-ui/react";
import React from "react";
import { AiFillDelete } from "react-icons/ai";

const DeleteIconButton = () => {
  return (
    <IconButton
      variant="outline"
      size="sm"
      fontSize="18px"
      colorScheme="red"
      icon={<AiFillDelete />}
      aria-label="Delete User"
    />
  );
};

export default DeleteIconButton;
