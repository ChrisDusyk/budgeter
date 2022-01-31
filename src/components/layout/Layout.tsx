import { Box } from "grommet";
import { useAuth0 } from "@auth0/auth0-react";
import { UnauthenticatedView } from "./Unauthenticated";
import { Authenticated } from "./Authenticated";

export function Layout() {
	const { isAuthenticated } = useAuth0();

	return <>{isAuthenticated ? <Authenticated /> : <UnauthenticatedView />}</>;
}
