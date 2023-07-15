#! /usr/bin/env bash

cd "$(dirname "$0")/../" || exit 1

cd EPR.EventDispatcher/EPR.EventDispatcher.Functions || exit 1

func start --verbose
