import { Button } from "grommet";
import { useAuth0 } from "@auth0/auth0-react";

export function LogoutButton() {
	const { logout } = useAuth0();

	return <Button onClick={() => logout({ returnTo: window.location.origin })}>Log out</Button>;
}
