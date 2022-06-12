import { Route, Routes } from "react-router-dom"
import Layout from "./components/layout/Layout"
import Login from "./pages/auth/Login"
import SignUp from "./pages/auth/SignUp"

export const App = () => (
  <Routes>
    <Route path="/" element={<Layout />}>
      {/* Public routes */}
      <Route path="login" element={<Login />} />
      <Route path="signup" element={<SignUp />} />
    </Route>
  </Routes>
)
