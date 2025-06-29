#~ .NET Project

.SILENT:
.NOTPARALLEL:
.ONESHELL:

all clean run test benchmark examples debug release package publish ~clean ~run ~test ~benchmark ~examples ~debug ~release ~package ~publish &:
	./Make.sh $(MAKECMDGOALS)
