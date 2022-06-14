import Api from "../Api/Api";
import { useEffect } from "react";
import useRefreshToken from "./useRefreshToken";
import useAuth from "./useAuth";

const useAxiosPrivate = () => {
  const refresh = useRefreshToken();
  const { auth } = useAuth();

  console.log("Adding Authorization Bearer token in header");
  console.log(auth);

  useEffect(() => {
    const requestIntercept = Api.interceptors.request.use(
      (config) => {
        if (!config.headers["Authorization"]) {
          config.headers["Authorization"] = `Bearer ${auth?.accessToken}`;
        }
        return config;
      },
      (error) => Promise.reject(error)
    );

    const responseIntercept = Api.interceptors.response.use(
      (response) => response,
      async (error) => {
        const prevRequest = error?.config;
        if (error?.response?.status === 401 && !prevRequest?.sent) {
          console.log('Token expired, getting new token');
          prevRequest.sent = true;
          const newAccessToken = await refresh();
          console.log("got new token: " + newAccessToken);
          prevRequest.headers["Authorization"] = `Bearer ${newAccessToken}`;
          return Api(prevRequest);
        }
        return Promise.reject(error);
      }
    );

    return () => {
      Api.interceptors.request.eject(requestIntercept);
      Api.interceptors.response.eject(responseIntercept);
    };
  }, [auth, refresh]);

  return Api;
};

export default useAxiosPrivate;
