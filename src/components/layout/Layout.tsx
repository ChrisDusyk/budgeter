import React from "react";
import { Grommet } from "grommet";
import { useAuth0 } from "@auth0/auth0-react";
import { UnauthenticatedLayout } from "./UnauthenticatedLayout";
import { AuthenticatedLayout } from "./AuthenticatedLayout";

export const Layout: React.FC<React.PropsWithChildren<unknown>> = ({ children }: React.PropsWithChildren<unknown>) => {
	const { isAuthenticated } = useAuth0();

	return <Grommet full>{isAuthenticated ? <AuthenticatedLayout>{children}</AuthenticatedLayout> : <UnauthenticatedLayout />}</Grommet>;
};
