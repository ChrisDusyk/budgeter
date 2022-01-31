import { Grommet } from "grommet";
import { useAuth0 } from "@auth0/auth0-react";
import { UnauthenticatedView } from "./Unauthenticated";
import { AuthenticatedView } from "./Authenticated";

export function Layout() {
	const { isAuthenticated } = useAuth0();

	return <Grommet full>{isAuthenticated ? <AuthenticatedView /> : <UnauthenticatedView />}</Grommet>;
}
