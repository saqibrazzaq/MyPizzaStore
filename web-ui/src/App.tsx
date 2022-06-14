import { Route, Routes } from "react-router-dom";
import AdminLayout from "./components/layout/AdminLayout";
import Layout from "./components/layout/Layout";
import PersistLogin from "./hooks/PersistLogin";
import RequireAuth from "./hooks/RequireAuth";
import AccountHome from "./pages/Account/AccountHome";
import AdminHome from "./pages/Admin/AdminHome";
import AdminListUsers from "./pages/Admin/AdminListUsers";
import RegisterAdmin from "./pages/Admin/RegisterAdmin";
import ChangePassword from "./pages/auth/ChangePassword";
import Login from "./pages/auth/Login";
import SignUp from "./pages/auth/SignUp";
import UnAuthorized from "./pages/auth/UnAuthorized";

export const App = () => {
  enum Roles {
    Admin = "Admin",
    Manager = "Manager",
    User = "User",
  }
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        {/* Public routes */}
        <Route path="login" element={<Login />} />
        <Route path="signup" element={<SignUp />} />
        <Route path="unauthorized" element={<UnAuthorized />} />

        {/* Admin Only routes */}
        <Route element={<PersistLogin />}>
          <Route
            element={
              <RequireAuth allowedRoles={[Roles.Admin, Roles.Manager]} />
            }
          >
            <Route path="admin" element={<AdminLayout />} >
              <Route index element={<AdminHome />} />
              <Route path="register-admin" element={<RegisterAdmin />} />
              <Route path="users" element={<AdminListUsers />} />
            </Route>
            
          </Route>
        </Route>

        {/* Manager routes */}

        {/* User routes */}
        <Route
          element={
            <RequireAuth
              allowedRoles={[Roles.Admin, Roles.Manager, Roles.User]}
            />
          }
        >
          <Route path="account">
            <Route index element={<AccountHome />} />
            <Route path="change-password" element={<ChangePassword />} />
          </Route>
        </Route>
      </Route>
    </Routes>
  );
};
