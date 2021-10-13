#
# Copyright (C) scenüs, 2012.
# All rights reserved.
# Ehsan Haghpanah; haghpanah@scenus.com
#

function packages_uninstall([bool]$exit_on_exception)
{
	$list=@{
	}

	foreach ($item in $list.GetEnumerator()) {
		foreach ($idol in $item.Value)
		{
			try
			{
				uninstall-package -id $idol -projectname $item.Name
			}
			catch
			{
				Write-Host "packages_uninstall"
				Write-Host $idol -foregroundcolor red -backgroundcolor yellow
				Write-Host $item.Name -foregroundcolor red -backgroundcolor white

				#
				# return back if flag set true
				if ($exit_on_exception)	{
					return $false
				}
			}
		}
	}

	return $true
}

function packages_reinstall([bool]$exit_on_exception)
{
	$list=@{
	}

	foreach ($item in $list.GetEnumerator()) {
		foreach ($idol in $item.Value)
		{
			try
			{
				install-package -id $idol -projectname $item.Name
			}
			catch
			{
				Write-Host "packages_reinstall"
				Write-Host $idol -foregroundcolor red -backgroundcolor yellow
				Write-Host $item.Name -foregroundcolor red -backgroundcolor white

				#
				# return back if flag set true
				if ($exit_on_exception)	{
					return $false
				}
			}
		}
	}

	return $true
}

function reinstall_packages
{
	install-package -id NLog -Version 4.4.9 -projectname Core\Base
}

#packages_uninstall $false
#packages_reinstall $false
reinstall_packages

##
## main function
#if (packages_uninstall -eq $true)
#{
#	if (packages_reinstall -eq $true)
#	{
#		Write-Host "packages-update-success" -foregroundcolor blue
#	}
#	else
#	{
#		Write-Host "packages-reinstall-failure" -foregroundcolor red
#	}
#}
#else
#{
#	Write-Host "packages-uninstall-failure" -foregroundcolor red
#}