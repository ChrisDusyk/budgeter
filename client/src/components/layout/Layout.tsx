import React from "react";
import { Grommet } from "grommet";
import { grommet } from "grommet/themes";
import { useAuth0 } from "@auth0/auth0-react";
import { UnauthenticatedLayout } from "./UnauthenticatedLayout";
import { AuthenticatedLayout } from "./AuthenticatedLayout";

export const Layout: React.FC<React.PropsWithChildren<unknown>> = ({ children }: React.PropsWithChildren<unknown>) => {
	const { isAuthenticated } = useAuth0();

	return <Grommet theme={grommet}>{isAuthenticated ? <AuthenticatedLayout>{children}</AuthenticatedLayout> : <UnauthenticatedLayout />}</Grommet>;
};
