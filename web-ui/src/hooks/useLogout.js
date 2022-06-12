import * as AuthService from "../Services/AuthService";
import useAuth from "./useAuth";

const useLogout = () => {
  const { setAuth } = useAuth();

  const logout = async () => {
    setAuth({});

    AuthService.logout()
      .then((res) => {
        console.log("logout successful");
      })
      .catch((err) => console.log("Cannot logout successfully"));
  };

  return logout;
};

export default useLogout;
