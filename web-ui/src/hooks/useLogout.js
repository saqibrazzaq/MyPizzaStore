import { useEffect } from "react";
import useAuth from "./useAuth";
import useAxiosPrivate from "./useAxiosPrivate";

const useLogout = () => {
  const { setAuth } = useAuth();
  const axios = useAxiosPrivate();

  const logout = async () => {
    
    setAuth({});

    axios.post("/User/logout")
      .then((res) => {
        console.log("logout successful");
      })
      .catch((err) => console.log("Cannot logout successfully"));
  };

  return logout;
};

export default useLogout;
